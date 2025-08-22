using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;

public class TurnHandler : MonoBehaviour, IInitializable
{
    [Inject] private ScoreHandler _scoreHandler;
    [Inject] private PieceInteraction _pieceInteraction;
    [Inject] private PieceMoveHandler _pieceMoveHandler;
    [Inject] private PieceVisualizer _pieceVisualizer;
    [Inject] private PieceHandler _pieceHandler;
    [Inject] private MatchHandler _matchHandler;
    public void Initialize()
    {
    }

    public void NextRound()
    {
        StartCoroutine(NextRoundIE());
    }
    private IEnumerator NextRoundIE()
    {
        List<Piece> pieces = new List<Piece>(_pieceInteraction.SelectedPieces);
        List<Piece> selectedPieces = new List<Piece>(_pieceInteraction.SelectedPieces);
        _pieceHandler.BlockAllInteractions();
        foreach (Piece piece in selectedPieces)
        {
            if (!_matchHandler.IsMyPiece(piece))
            {
                print("girdi1");
                yield return _pieceMoveHandler.Reject(piece).WaitForCompletion();
                print("girdi2");
                pieces.Remove(piece);
            }
        }
        yield return new WaitForSeconds(.5f);
        yield return _pieceMoveHandler.RePosPieceSelected(selectedPieces);
        yield return new WaitForSeconds(.5f);
        foreach (Piece piece in selectedPieces)
        {
            yield return _pieceMoveHandler.Drop(piece, .4f).WaitForCompletion();
            yield return _pieceVisualizer.Flip(piece).WaitForCompletion();
        }
        yield return new WaitForSeconds(.5f);
        int counter = 0;
        List<RolledDiceData> rolledDiceData = _matchHandler.GetMatches();
        for (int i = 0; i < rolledDiceData.Count; i++)
        {
            if (selectedPieces.Contains(rolledDiceData[i].MyPiece))
            {
                print(rolledDiceData[i].MyPiece);
                print(rolledDiceData[i].Calc);
                _scoreHandler.AddScore(rolledDiceData[i].MyPiece.ChipAmount, _matchHandler.CalcCount(ref counter));
                yield return _pieceMoveHandler.Punch(rolledDiceData[i].MyPiece).WaitForCompletion();
            }

        }
        yield return new WaitForSeconds(.5f);
        _scoreHandler.SetScore();
        yield return new WaitForSeconds(.5f);
        foreach (Piece piece in selectedPieces)
        {
            _pieceMoveHandler.ToHand(piece);
            _pieceHandler.RemoveFromHand(piece);
            yield return new WaitForSeconds(.1f);
        }
        counter = 0;
        yield return new WaitForSeconds(1f);
        _pieceInteraction.ClearSelectedHand();
        _pieceHandler.OpenAllInteractions();
    }
}