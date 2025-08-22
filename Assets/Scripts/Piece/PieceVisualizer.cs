using DG.Tweening;
using UnityEngine;
using Zenject;

public class PieceVisualizer : MonoBehaviour, IInitializable
{
    [Header("---Piece Props")]
    [SerializeField] private Sprite _selected;
    [SerializeField] private Sprite _unSelected;
    [Header("---DoTween Props")]
    [SerializeField] private float _flipDuration;
    public Tween Flip(Piece piece)
    {
        return piece.GetFrontFace().transform.DOLocalRotate(Vector3.up * 90, _flipDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            piece.GetFrontFace().SetActive(false);
            piece.GetBackFace().SetActive(true);
        });
    }
    public void PressSprite(Piece piece)
    {
        piece.GetSP().sprite = _selected;
    }
    public void UnPressSprite(Piece piece)
    {
        piece.GetSP().sprite = _unSelected;
    }
    public void Initialize()
    {
        EventManager.Subscribe<MouseEnterEvent2D>(ShowPieceDataUI);
        EventManager.Subscribe<MouseExitEvent2D>(DisablePieceDataUI);
    }
    void OnDestroy()
    {
        EventManager.UnSubscribe<MouseEnterEvent2D>(ShowPieceDataUI);
        EventManager.UnSubscribe<MouseExitEvent2D>(DisablePieceDataUI);

    }
    public void ShowPieceDataUI(MouseEnterEvent2D data)
    {
        if (data.MouseObject.TryGetComponent<Piece>(out Piece piece))
        {
            piece.GetBackFace().SetActive(true);
        }
    }
    public void DisablePieceDataUI(MouseExitEvent2D data)
    {
        if (data.MouseObject.TryGetComponent<Piece>(out Piece piece))
        {
            piece.GetBackFace().SetActive(false);
        }
    }
}