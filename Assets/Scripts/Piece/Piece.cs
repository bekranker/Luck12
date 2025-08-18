using TMPro;
using UnityEngine;

public class Piece : MonoBehaviour
{

    [Header("---UI Props")]
    [SerializeField] private TMP_Text _name;
    [SerializeField] private TMP_Text _chipCount;
    [SerializeField] private GameObject _normalFace;
    [SerializeField] private GameObject _backFace;

    private PieceData _data;
    public bool CanInteract;
    public bool Selected;
    public Vector2 GridPosition;


    public PieceData GetData() => _data;
    public int ChipAmount;
    public GameObject BackFace => _backFace;
    public GameObject FrontFace => _normalFace;

    public void InitPiece(PieceData piece)
    {
        _data = piece;
        _name.text = _data.Number.ToString();

        ChipAmount = Random.Range(10, 100);
        _chipCount.text = ChipAmount.ToString();

        _backFace.SetActive(false);
    }
}