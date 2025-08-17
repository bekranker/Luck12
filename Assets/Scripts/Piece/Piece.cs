using TMPro;
using UnityEngine;

public class Piece : MonoBehaviour
{

    [Header("---UI Props")]
    [SerializeField] private TMP_Text _name;
    [SerializeField] private GameObject _normalFace;
    [SerializeField] private GameObject _backFace;

    private PieceData _data;
    private PieceMoveHandler _PieceMoveHandler;
    private PieceHandler _pieceHandler;
    public bool CanInteract;
    public bool Selected;
    public Vector2 GridPosition;
    public PieceData GetData() => _data;
    public void InitPiece(PieceHandler pieceHandler, PieceData piece, PieceMoveHandler pieceVisulazier)
    {
        _data = piece;
        _pieceHandler = pieceHandler;
        _PieceMoveHandler = pieceVisulazier;
        _name.text = _data.Number.ToString();
        _backFace.SetActive(false);
    }
}