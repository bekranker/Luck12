using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
using UnityEngine.Events;

public class CustomButton : MonoBehaviour,
    IPointerEnterHandler,
    IPointerExitHandler,
    IPointerDownHandler,
    IPointerUpHandler
{
    public UnityEvent OnUp, OnDown;
    [SerializeField] private Transform _spriteT;
    [SerializeField] private float _downScale = 0.9f;
    [SerializeField] private float _downDuration = 0.1f;
    [SerializeField] private float _punchScale = 0.2f;
    [SerializeField] private float _punchDuration = 0.3f;
    [SerializeField] private float _punchRotation = 15f; // z ekseninde derece

    private bool _entered;
    private Vector3 _originalScale;
    private Vector3 _originalRotation;

    private void Awake()
    {
        if (_spriteT == null) _spriteT = transform;
        _originalScale = _spriteT.localScale;
        _originalRotation = _spriteT.localEulerAngles;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _entered = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _entered = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!_entered) return;
        DOTween.Kill(_spriteT);

        _spriteT.DOScale(_originalScale * _downScale, _downDuration)
            .SetEase(Ease.OutQuad);

        OnDown?.Invoke();

    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (!_entered) return;
        DOTween.Kill(_spriteT);

        // reset scale + rotasyon
        _spriteT.localScale = _originalScale;
        _spriteT.localEulerAngles = _originalRotation;

        // scale punch
        _spriteT.DOPunchScale(Vector3.one * _punchScale, _punchDuration, 8, 0.5f);

        // rotation punch sadece Z ekseninde
        _spriteT.DOPunchRotation(new Vector3(0, 0, _punchRotation * Random.Range(-1, 2)), _punchDuration, 8, 0.5f);

        OnUp?.Invoke();
    }
}
