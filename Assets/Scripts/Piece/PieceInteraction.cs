using UnityEngine;
using Zenject;

public class PieceInteraction : MonoBehaviour, IInitializable
{
    [Inject] private PieceMoveHandler _pieceMoveHandler;
    [Inject] private MatchHandler _matchHandler;
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
                if (!_matchHandler.GetMatches().Contains(piece))
                {
                    _pieceMoveHandler.Reject(piece);
                    return;
                }
                _pieceMoveHandler.RaiseUp(piece);
                piece.Selected = true;
            }
            else
            {
                _pieceMoveHandler.Drop(piece);
                piece.Selected = false;
            }
        }
    }
}