using TMPro;
using UnityEngine;

public class Piece : MonoBehaviour
{

    [Header("---UI Props")]
    [SerializeField] private TMP_Text _name;
    private PieceData _data;
    private PieceVisualizer _pieceVisualizer;
    private PieceHandler _pieceHandler;


    public PieceData GetData() => _data;
    public void InitPiece(PieceHandler pieceHandler, PieceData piece, PieceVisualizer pieceVisulazier)
    {
        _data = piece;
        _pieceHandler = pieceHandler;
        _pieceVisualizer = pieceVisulazier;
        _name.text = _data.Number.ToString();
    }
}