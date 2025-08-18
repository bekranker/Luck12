using DG.Tweening;
using UnityEngine;
using Zenject;

public class PieceVisualizer : MonoBehaviour, IInitializable
{
    [Header("---DoTween Props")]
    [SerializeField] private float _flipDuration;
    public Tween Flip(Piece piece)
    {
        return piece.FrontFace.transform.DOLocalRotate(Vector3.up * 90, _flipDuration).SetEase(Ease.Linear).OnComplete(() =>
        {
            piece.FrontFace.SetActive(false);
            piece.BackFace.SetActive(true);
        });
    }

    public void Initialize()
    {
    }
}