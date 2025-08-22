using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PieceInteraction : MonoBehaviour, IInitializable
{
    [Inject] private PieceMoveHandler _pieceMoveHandler;
    [Inject] private PieceVisualizer _pieceVisualizer;

    public List<Piece> SelectedPieces = new();
    public void Initialize()
    {
        EventManager.Subscribe<MouseDownEvent2D>(GetPieceDown);
    }
    void OnDestroy()
    {
        EventManager.UnSubscribe<MouseDownEvent2D>(GetPieceDown);
    }
    void GetPieceDown(MouseDownEvent2D data)
    {
        if (data.MouseObject.TryGetComponent(out Piece piece))
        {
            if (!piece.Selected)
            {
                if (!SelectedPieces.Contains(piece))
                    SelectedPieces.Add(piece);
                _pieceVisualizer.PressSprite(piece);
                piece.Selected = true;
            }
            else
            {
                if (SelectedPieces.Contains(piece))
                    SelectedPieces.Remove(piece);
                _pieceVisualizer.UnPressSprite(piece);
                piece.Selected = false;
            }
        }
    }
    public void ClearSelectedHand() => SelectedPieces?.Clear();
}