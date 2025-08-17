using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class MatchHandler : MonoBehaviour, IInitializable
{
    [Inject] private PieceHandler _pieceHandler;
    private List<Piece> _matches = new();
    public void Initialize()
    {
        print("Match Handler Initialized");
        EventManager.Subscribe<DiceRolled>(TakeRollIndexes);
    }
    void OnDestroy()
    {
        EventManager.UnSubscribe<DiceRolled>(TakeRollIndexes);
    }
    public void TakeRollIndexes(DiceRolled data)
    {
        _matches.Clear();
        foreach (int index in data.Numbers)
        {
            foreach (int otherIndex in data.Numbers)
            {
                if (otherIndex != index)
                {
                    if (index % otherIndex == 0)
                        _matches.Add(_pieceHandler.SelectPiece(index % otherIndex));
                    _matches.Add(_pieceHandler.SelectPiece(index * otherIndex));
                    _matches.Add(_pieceHandler.SelectPiece(index + otherIndex));
                    _matches.Add(_pieceHandler.SelectPiece(index - otherIndex));

                }
            }

        }
        PrintResult();
    }
    private void PrintResult()
    {
        foreach (Piece piece in _matches)
        {
            if (piece != null)
                Debug.Log($"{piece.GetData().Number}");
        }
    }
    public List<Piece> GetMatches() => _matches;
}