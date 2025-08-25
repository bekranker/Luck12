using DG.Tweening;
using UnityEngine;
using Zenject;

public class RuneVisualizer : MonoBehaviour, IInitializable
{

    public void Initialize()
    {
        EventManager.Subscribe<MouseEnterEvent2D>(ShowRuneUI);
        EventManager.Subscribe<MouseExitEvent2D>(HideRuneUI);
    }
    void OnDestroy()
    {
        EventManager.UnSubscribe<MouseEnterEvent2D>(ShowRuneUI);
        EventManager.UnSubscribe<MouseExitEvent2D>(HideRuneUI);
    }
    public void ShowRuneUI(MouseEnterEvent2D data)
    {
        if (data.MouseObject.TryGetComponent(out Rune rune))
        {
            rune.GetRuneUI().SetActive(true);
            DOTween.Kill(rune.transform);
            rune.transform.localScale = Vector3.one;
            rune.transform.DOPunchScale(Vector3.one * .2f, .2f);
        }
    }
    public void HideRuneUI(MouseExitEvent2D data)
    {
        if (data.MouseObject.TryGetComponent(out Rune rune))
        {
            rune.GetRuneUI().SetActive(false);
        }
    }
    public Tween PunchScale(Rune rune)
    {
        print("punched");
        DOTween.Kill(rune.transform);
        rune.transform.localScale = Vector3.one;
        return rune.transform.DOPunchScale(Vector3.one * .2f, .2f);
    }
}
