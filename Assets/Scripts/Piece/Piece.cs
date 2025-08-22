using TMPro;
using UnityEngine;

public class Piece : MonoBehaviour
{

    [Header("---UI Props")]
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _name2;
    [SerializeField] private TMP_Text _chipCount;
    [SerializeField] private GameObject _normalFace;
    [SerializeField] private GameObject _backFace;
    [SerializeField] private SpriteRenderer _spriteRenderer;
    private PieceData _data;
    public bool CanInteract;
    public bool Selected;
    public Vector2 GridPosition;


    public PieceData GetData() => _data;
    public SpriteRenderer GetSP() => _spriteRenderer;
    public int ChipAmount;
    public GameObject GetBackFace() => _backFace;
    public GameObject GetFrontFace() => _normalFace;

    public void InitPiece(PieceData piece)
    {
        _data = piece;
        _name.text = _data.Number.ToString();
        _name2.text = _data.Number.ToString();

        ChipAmount = _data.ChipCount;
        _chipCount.text = ChipAmount.ToString() + " Chip";

        _backFace.SetActive(false);
    }
}