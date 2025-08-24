using DG.Tweening;
using UnityEngine.UI;
using TMPro;
using UnityEngine;

public class Rune : MonoBehaviour
{
    [Header("----UI Props")]
    [SerializeField] private TMP_Text _description;
    [SerializeField] private TMP_Text _name;
    [SerializeField] private GameObject _runeUI;
    [SerializeField] private Image _sprite;
    private RuneData _data;
    public RuneData GetData() => _data;
    public void Initialize(RuneData data)
    {
        _sprite.sprite = data.SpriteValue;
        _name.text = data.Name;
        _description.text = data.Description;
        _runeUI.SetActive(false);
    }

    public GameObject GetRuneUI() => _runeUI;
}