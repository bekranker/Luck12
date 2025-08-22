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
        _currentScore += amount * _currentMultipilier;
        SetTMP(_currentScore, mult);
    }

    private void SetTMP(float score, int mult)
    {
        _scoreTMP.text = score.ToString() + " x" + mult.ToString(); ;
    }
}