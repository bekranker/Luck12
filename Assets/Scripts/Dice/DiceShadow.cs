using UnityEngine;

public class DiceShadow : MonoBehaviour
{
    [SerializeField] private float _scaleAmount;
    [SerializeField] private Transform _shadowSprite;
    [SerializeField] private float _maxShadowSize;
    [SerializeField] private float _minShadowSize = 0.3f;
    void Update()
    {
        SetShadowScale();
        SetShadowPos();
        SetShadowRotation();
    }
    private void SetShadowScale()
    {
        float currentHeight = transform.localPosition.z;

        Vector2 targetScale = Vector2.one * _scaleAmount / currentHeight;
        targetScale.x = Mathf.Clamp(targetScale.x, _minShadowSize, _maxShadowSize);
        targetScale.y = Mathf.Clamp(targetScale.y, _minShadowSize, _maxShadowSize);
        _shadowSprite.localScale = targetScale;
    }

    private void SetShadowPos()
    {
        _shadowSprite.position = (Vector2)transform.position;
    }
    private void SetShadowRotation()
    {
        Vector3 targetEuler = transform.eulerAngles; // kaynağın rotasyonu
        Vector3 shadowEuler = _shadowSprite.eulerAngles; // mevcut shadow rotasyonu

        // sadece X ve Z'yi eşitle, Y sabit kalsın
        _shadowSprite.rotation = Quaternion.Euler(
            shadowEuler.x,
            shadowEuler.y,
            targetEuler.z
        );
    }


}