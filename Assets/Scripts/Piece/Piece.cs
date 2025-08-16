using TMPro;
using UnityEngine;

public class Piece : MonoBehaviour
{

    [Header("---UI Props")]
    [SerializeField] private TMP_Text _name;
    [SerializeField] private GameObject _normalFace;
    [SerializeField] private GameObject _backFace;

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
        _backFace.SetActive(false);
    }
}