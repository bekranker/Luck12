using UnityEngine;
using Zenject;

public class MatchHandler : MonoBehaviour
{
    [Inject] private DiceHandler _diceHandler;
    [Inject] private PieceHandler _pieceHandler;
}