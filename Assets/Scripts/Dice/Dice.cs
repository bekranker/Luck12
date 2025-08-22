using DG.Tweening;
using UnityEngine;

public class Dice : MonoBehaviour
{
    [Header("Dice Faces (index = number-1)")]
    [SerializeField] private Transform[] faceRotations;
    // Inspector’a 6 empty transform koy. 
    // Örn: faceRotations[0] => "1 yüzü yukarı bakacak rotation"
    // Bu transformların sadece rotation kısmı kullanılacak.

    public bool _showResult;
    public Vector3 LocalStartPos;

    void Start()
    {
        LocalStartPos = transform.localPosition;
    }

    /// <summary>
    /// Zarın yüzünü belirli sayıya döndür
    /// </summary>
    public void ShowResult(int number, float duration)
    {
        if (number < 1 || number > 6) return;
        // Inspector’da tanımlanan yüzün local rotation’ını alıyoruz
        Quaternion targetLocalRot = faceRotations[number - 1].localRotation;

        // DOTween ile local rotation'a dön
        transform.DOLocalRotateQuaternion(targetLocalRot, duration)
            .SetEase(Ease.OutSine);
    }

    public void KillTween()
    {
        DOTween.Kill(transform);
    }
    public void ReturnStartPosition()
    {
        DOTween.Kill(transform);
        transform.DOLocalMove(LocalStartPos, .2f).SetEase(Ease.OutSine);
    }
    public void StartSpin(Vector3 axis, float initialSpeed, float duration)
    {
        // Tween kullanarak speed’i zamanla sıfıra indiriyoruz
        float speed = initialSpeed;

        DOTween.Kill(this); // varsa önceki spin’i durdur

        DOTween.To(() => speed, x => speed = x, 0f, duration)
            .SetEase(Ease.OutCubic)
            .OnUpdate(() =>
            {
                transform.Rotate(axis * speed * Time.deltaTime, Space.Self);
            })
            .SetId(this);
    }
}