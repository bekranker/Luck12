using UnityEngine;
using DG.Tweening;
using Zenject;

public class RuneMoveHandler : MonoBehaviour, IInitializable
{
    [Header("---DoTween Props")]
    [SerializeField] private float _handPunchDuration;
    [SerializeField] private float _handPunchScale;
    [SerializeField] private Ease _punchEase;

    public void Initialize()
    {
    }

    public void ToHand(Rune rune)
    {
        rune.transform.DOPunchScale(Vector2.one * _handPunchScale, _handPunchDuration).SetEase(_punchEase).OnComplete(() =>
        {
            rune.transform.localPosition = new Vector3(rune.transform.localPosition.x, 0, rune.transform.localPosition.z);
            rune.transform.localScale = Vector3.one;
        });
    }
}