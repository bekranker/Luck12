using System.Collections;
using TMPro;
using UnityEngine;
using Zenject;

public class ScoreHandler : MonoBehaviour, IInitializable
{
    [Header("---Props")]
    [SerializeField] private float _duration;
    [SerializeField] private TMP_Text _scoreTMP;
    [SerializeField] private float _targetScore;

    private float _currentScore;
    private float _currentMultipilier = 1f;


    public void Initialize()
    {
    }

    public void AddScore(float amount, int mult)
    {
        StartCoroutine(AddScoreIE(amount * mult));
    }
    private IEnumerator AddScoreIE(float amount)
    {
        float targetScore = _currentScore + amount * _currentMultipilier;
        while (_currentScore <= targetScore)
        {
            _currentScore++;
            SetTMP(_currentScore);
            yield return new WaitForSeconds(_duration);
        }
    }
    private void SetTMP(float score)
    {
        _scoreTMP.text = score.ToString();
    }
}