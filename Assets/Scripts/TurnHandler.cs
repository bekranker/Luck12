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
    public void Initialize()
    {
    }

    public void NextRound()
    {
        StartCoroutine(NextRoundIE());
    }
    private IEnumerator NextRoundIE()
    {
        List<Piece> pieces = _pieceInteraction.SelectedPieces;
        int calcCount = 0;
        _pieceHandler.BlockAllInteractions();
        yield return _pieceMoveHandler.RePosPieceSelected(pieces);
        yield return new WaitForSeconds(.5f);
        foreach (Piece piece in pieces)
        {
            yield return _pieceMoveHandler.Drop(piece, .4f).WaitForCompletion();
            yield return _pieceVisualizer.Flip(piece).WaitForCompletion();
        }
        yield return new WaitForSeconds(.5f);
        foreach (Piece piece in pieces)
        {
            calcCount++;
            piece.CanInteract = false;
            _scoreHandler.AddScore(piece.ChipAmount, calcCount);
            yield return _pieceMoveHandler.Punch(piece).WaitForCompletion();
        }
        yield return new WaitForSeconds(.5f);
        foreach (Piece piece in pieces)
        {
            _pieceMoveHandler.ToHand(piece);
            yield return new WaitForSeconds(.1f);
        }
        yield return new WaitForSeconds(1f);
        _pieceHandler.OpenAllInteractions();
    }
}