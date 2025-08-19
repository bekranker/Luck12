using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using Zenject;
public class MatchHandler : MonoBehaviour, IInitializable
{
    [Inject] private PieceHandler _pieceHandler;
    private List<RolledDiceData> _matches = new();

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
                    Piece piece = _pieceHandler.SelectPiece(index / otherIndex);
                    RolledDiceData dataDivide = new RolledDiceData(piece, CalculationType.Divide);
                    if (piece != null && !_matches.Contains(dataDivide))
                        _matches.Add(dataDivide);
                }
                else if (otherIndex % index == 0)
                {

                    Piece piece = _pieceHandler.SelectPiece(otherIndex / index);
                    RolledDiceData dataDivide2 = new RolledDiceData(piece, CalculationType.Divide);
                    if (piece != null && !_matches.Contains(dataDivide2))
                        _matches.Add(dataDivide2);
                }

                // toplama
                Piece pieceSum = _pieceHandler.SelectPiece(index + otherIndex);
                RolledDiceData dataSum = new RolledDiceData(pieceSum, CalculationType.Sum);

                if (pieceSum != null && !_matches.Contains(dataSum))
                    _matches.Add(dataSum);

                // çıkarma (iki yönde)

                Piece pieceDiff1 = _pieceHandler.SelectPiece(index - otherIndex);
                Piece pieceDiff2 = _pieceHandler.SelectPiece(otherIndex - index);
                RolledDiceData dataMighness = new RolledDiceData(pieceDiff1, CalculationType.Mighness);
                RolledDiceData dataMighness2 = new RolledDiceData(pieceDiff2, CalculationType.Mighness);
                if (pieceDiff1 != null && !_matches.Contains(dataMighness))
                    _matches.Add(dataMighness);
                if (pieceDiff2 != null && !_matches.Contains(dataMighness2))
                    _matches.Add(dataMighness2);

                // çarpma
                Piece pieceMult = _pieceHandler.SelectPiece(index * otherIndex);
                RolledDiceData dataMult = new RolledDiceData(pieceMult, CalculationType.Multipily);
                print(pieceMult + " ");
                // if (pieceMult != null && !_matches.Contains(dataMult))
                _matches.Add(dataMult);
            }
        }
    }

    public List<RolledDiceData> GetMatches() => _matches;
    private void PrintResult()
    {
        foreach (RolledDiceData data in _matches)
        {
            if (data != null && data.MyPiece)
                Debug.Log($"{data.MyPiece.GetData().Number}");
        }
    }
    public bool IsMyPiece(Piece piece)
    {
        foreach (RolledDiceData data in GetMatches())
        {
            if (data.MyPiece == piece) return true;
        }
        return false;
    }
    public int CalcCount(ref int counter)
    {
        foreach (RolledDiceData data in GetMatches())
        {
            if (data.Calc == CalculationType.Sum)
            {
                //Invoke Sum Events
            }
            else if (data.Calc == CalculationType.Mighness)
            {
                //Invoke Mighness Events
            }
            else if (data.Calc == CalculationType.Multipily)
            {
                //Invoke Mult Events
            }
            else if (data.Calc == CalculationType.Divide)
            {
                //Invoke Divide Events
            }
            counter++;
        }

        return counter;
    }
}