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
        _matches?.Clear();

        for (int i = 0; i < data.Numbers.Count; i++)
        {
            int index = data.Numbers[i];
            for (int j = i + 1; j < data.Numbers.Count; j++) // << değişiklik burada
            {
                int otherIndex = data.Numbers[j];

                if (otherIndex == 0) continue; // güvenlik

                // bölme (her iki yönde de bakabilirsin)
                if (index % otherIndex == 0)
                {
                    var piece = _pieceHandler.SelectPiece(index / otherIndex);
                    if (piece != null && !_matches.Contains(piece))
                        _matches.Add(piece);
                }
                if (otherIndex % index == 0)
                {
                    var piece = _pieceHandler.SelectPiece(otherIndex / index);
                    if (piece != null && !_matches.Contains(piece))
                        _matches.Add(piece);
                }

                // toplama
                var pieceSum = _pieceHandler.SelectPiece(index + otherIndex);
                if (pieceSum != null && !_matches.Contains(pieceSum))
                    _matches.Add(pieceSum);

                // çıkarma (iki yönde)
                var pieceDiff1 = _pieceHandler.SelectPiece(index - otherIndex);
                var pieceDiff2 = _pieceHandler.SelectPiece(otherIndex - index);
                if (pieceDiff1 != null && !_matches.Contains(pieceDiff1))
                    _matches.Add(pieceDiff1);
                if (pieceDiff2 != null && !_matches.Contains(pieceDiff2))
                    _matches.Add(pieceDiff2);

                // çarpma
                var pieceMult = _pieceHandler.SelectPiece(index * otherIndex);
                if (pieceMult != null && !_matches.Contains(pieceMult))
                    _matches.Add(pieceMult);
            }
        }

    }

    public List<Piece> GetMatches() => _matches;
    private void PrintResult()
    {
        foreach (Piece piece in _matches)
        {
            if (piece != null)
                Debug.Log($"{piece.GetData().Number}");
        }
    }
}