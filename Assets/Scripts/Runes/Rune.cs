using DG.Tweening;
using TMPro;
using UnityEngine;

public class Rune : MonoBehaviour
{
    [Header("----UI Props")]
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private GameObject _rune;



    public void Initialize()
    {

    }

    void OnEnable()
    {
        EventManager.Subscribe<MouseEnterEvent2D>(ShowDescUI);
        EventManager.Subscribe<MouseExitEvent2D>(HideDescUI);
    }
    void OnDestroy()
    {
        EventManager.UnSubscribe<MouseEnterEvent2D>(ShowDescUI);
        EventManager.UnSubscribe<MouseExitEvent2D>(HideDescUI);
    }

    public void ShowDescUI(MouseEnterEvent2D data)
    {
        _rune.SetActive(true);
        DOTween.Kill(_rune.transform);
        _rune.transform.localScale = Vector2.one;
        _rune.transform.DOPunchScale(Vector2.one * .2f, .2f);
    }
    public void HideDescUI(MouseExitEvent2D data)
    {
        _rune.SetActive(false);
    }
}