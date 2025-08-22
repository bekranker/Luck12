using System.Collections;
using DG.Tweening;
using TMPro;
using UnityEngine;
using Zenject;

public class ScoreHandler : MonoBehaviour, IInitializable
{
    [Header("---Colors")]
    [SerializeField] private Color _currentChipColor;
    [SerializeField] private Color _currentMultColor;

    [Header("---Props")]
    [SerializeField] private float _duration;
    [SerializeField] private TMP_Text _scoreTMP;
    [SerializeField] private float _targetScore;

    private float _currentScore;
    private float _currentMultipilier = 1f;
    private Vector3 _initalScale;

    public void Initialize()
    {
        _initalScale = _scoreTMP.transform.localScale;
    }
    public void AddScore(float amount, int mult)
    {
        _currentScore += amount * _currentMultipilier;
        SetTMP(amount, mult, true);
    }
    public void SetScore()
    {
        StartCoroutine(SetScoreIE());
    }
    private IEnumerator SetScoreIE()
    {
        float currentAddedScore = 0;

        // local step (targetScore’a göre hesaplanıyor)

        float step = _currentScore / 100f;

        while (currentAddedScore < _currentScore)
        {
            currentAddedScore += step;

            if (currentAddedScore > _currentScore)
                currentAddedScore = _currentScore;

            SetTMP(currentAddedScore, 1);

            yield return new WaitForSeconds(0.02f);
        }
    }
    private void SetTMP(float score, int mult, bool multVisisble = false)
    {
        DOTween.Kill(_scoreTMP.transform);
        _scoreTMP.transform.localScale = _initalScale;
        _scoreTMP.transform.DOPunchScale(Vector2.one * .2f, .2f);
        if (multVisisble)
            _scoreTMP.text = $"<color=#{ColorUtility.ToHtmlStringRGB(_currentChipColor)}>{score.ToString("0.00")}</color> <color=#{ColorUtility.ToHtmlStringRGB(_currentMultColor)}> x{mult.ToString()}</color>";

        else
            _scoreTMP.text = $"<color=#{ColorUtility.ToHtmlStringRGB(_currentChipColor)}>{score.ToString("0.00")}</color>";
    }
}