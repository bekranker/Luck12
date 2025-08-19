using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class PieceInteraction : MonoBehaviour, IInitializable
{
    [Inject] private PieceMoveHandler _pieceMoveHandler;

    public List<Piece> SelectedPieces = new();
    public void Initialize()
    {
        print("Piece Interaction Initialized");

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
                _pieceMoveHandler.RaiseUp(piece);
                piece.Selected = true;
            }
            else
            {
                if (SelectedPieces.Contains(piece))
                    SelectedPieces.Remove(piece);
                _pieceMoveHandler.Drop(piece);
                piece.Selected = false;
            }
        }
    }
    public void ClearSelectedHand() => SelectedPieces?.Clear();
}