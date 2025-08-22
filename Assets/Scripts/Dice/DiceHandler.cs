using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;
using DG.Tweening;

public class DiceHandler : MonoBehaviour, IInitializable
{
    [SerializeField] private List<Dice> _dices;
    [SerializeField] private List<TMP_Text> _diceTexts;
    [SerializeField] private Transform _diceParent;

    private bool _dragging;
    private Vector3 _lastMouseScreenPos;
    private List<Vector3> _velocities = new();

    [Header("Throw Settings")]
    [SerializeField] private float throwForce = 5f;
    [SerializeField] private float rollDuration = 2f; // toplam sekme süresi
    [SerializeField] private Vector2 screenBounds = new Vector2(8f, 4f); // ekran sınırları

    [Header("Spin Settings")]
    [SerializeField] private float spinDuration = 2f; // toplam spin süresi
    [SerializeField] private float spinSpeed = 720f;  // derece/saniye hız
    [SerializeField] private float settleDuration = 0.5f; // doğru yüze oturma süresi


    public List<int> _targetResults = new();

    private Vector3 _refVel;

    public void Initialize()
    {
        EventManager.Subscribe<MouseDownEvent3D>(DragDiceDown);
        EventManager.Subscribe<MouseUpEvent3D>(DragDiceUp);

        GenerateRandomResults();
    }

    void OnDestroy()
    {
        EventManager.UnSubscribe<MouseDownEvent3D>(DragDiceDown);
        EventManager.UnSubscribe<MouseUpEvent3D>(DragDiceUp);
    }

    void Update()
    {
        if (_dragging)
        {
            FollowMouse();
            return;
        }

        // Sekme hareketi
        for (int i = 0; i < _dices.Count; i++)
        {
            if (_velocities.Count <= i) continue;

            var dice = _dices[i];
            dice.transform.position += _velocities[i] * Time.deltaTime;

            Vector3 pos = dice.transform.position;
            Vector3 vel = _velocities[i];

            // Ekran sınırlarına çarpma kontrolü
            if (Mathf.Abs(pos.x) > screenBounds.x)
            {
                pos.x = Mathf.Sign(pos.x) * screenBounds.x;
                vel.x *= -1;
            }
            if (Mathf.Abs(pos.y) > screenBounds.y)
            {
                pos.y = Mathf.Sign(pos.y) * screenBounds.y;
                vel.y *= -1;
            }

            dice.transform.position = pos;

            // 💡 Velocity’yi yavaşlat (damping)
            float damping = 0.994f; // 1’e yakınsa yavaş yavaş durur, 0.9 hızlı durur
            vel *= damping;

            // Çok küçükse sıfırla
            if (vel.magnitude < 0.01f)
                vel = Vector3.zero;

            _velocities[i] = vel;
        }
    }

    private void DragDiceDown(MouseDownEvent3D data)
    {
        if (data.MouseObject == null) return;
        _lastMouseScreenPos = Input.mousePosition;
        _dragging = true;

        // Parent’ı sıfıra kaydır (DOTween ile smooth)
        foreach (Dice dice in _dices)
        {
            dice.ReturnStartPosition();
        }
    }


    private void FollowMouse()
    {
        Vector3 mouseScreen = Input.mousePosition;
        float zDist = Mathf.Abs(Camera.main.WorldToScreenPoint(_diceParent.position).z);

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(
            new Vector3(mouseScreen.x, mouseScreen.y, zDist)
        );

        _diceParent.position = Vector3.SmoothDamp(
            _diceParent.position,
            mouseWorld,
            ref _refVel,
            0.2f
        );
    }

    private void DragDiceUp(MouseUpEvent3D data)
    {
        if (!_dragging) return;
        _dragging = false;

        Vector3 releaseMousePos = Input.mousePosition;
        Vector3 screenDelta = releaseMousePos - _lastMouseScreenPos;

        _velocities.Clear();
        for (int i = 0; i < _dices.Count; i++)
        {
            _dices[i].KillTween();
            Vector3 vel = new Vector3(screenDelta.x, screenDelta.y, 0f).normalized * throwForce;
            _velocities.Add(vel);

            // 🎲 Spin başlat
            StartRollingAnimation(_dices[i]);
        }

        GenerateRandomResults();
        StartCoroutine(FinishRoll());
    }

    private void StartRollingAnimation(Dice dice)
    {
        Vector3 randomAxis = Random.onUnitSphere;
        dice.transform.DOLocalRotate(
            randomAxis * spinSpeed,       // inspectordan ayarlanabilir hız
            spinDuration,                  // inspectordan ayarlanabilir süre
            RotateMode.FastBeyond360
        )
        .SetLoops(-1, LoopType.Incremental)
        .SetEase(Ease.Linear)
        .SetId(dice);
    }


    private void StopRollingAnimation(Dice dice, int result)
    {
        DOTween.Kill(dice); // Spin’i durdur

        // DoTween ile doğru yüzeye kaydır
        dice.ShowResult(result, settleDuration);
    }


    private IEnumerator FinishRoll()
    {
        yield return new WaitForSeconds(spinDuration);

        for (int i = 0; i < _dices.Count; i++)
        {
            int result = _targetResults[i];
            _diceTexts[i].text = result.ToString();

            StopRollingAnimation(_dices[i], result);
            _velocities[i] = Vector3.zero;
        }

        EventManager.Raise(new DiceRolled(_targetResults));
    }



    // 🎲 Random sonuç üret
    public void GenerateRandomResults()
    {
        _targetResults.Clear();
        for (int i = 0; i < _dices.Count; i++)
            _targetResults.Add(Random.Range(1, 7));
    }

    // 🎯 Manuel sonuç set et
    public void SetTargetResults(List<int> results)
    {
        _targetResults.Clear();
        _targetResults.AddRange(results);
    }
}
