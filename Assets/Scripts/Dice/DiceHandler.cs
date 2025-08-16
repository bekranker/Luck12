using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Zenject;
using DG.Tweening;
using System.Collections;

public class DiceHandler : MonoBehaviour, IInitializable
{
    [SerializeField] private List<Dice> _dices;
    [SerializeField] private List<TMP_Text> _diceTexts;
    [SerializeField] private Transform _diceParent;

    private bool _resultsRecorded = false;
    private Vector3 _velocity = Vector3.zero;
    private bool _dragging;

    // Mouse tracking
    private Vector3 _lastMouseScreenPos;
    private Vector3 _mouseDirection;
    private float _mouseSpeed;
    private Vector3 _mouseVelocity;

    [SerializeField] private float throwForce = 0.05f;
    [SerializeField] private float velocityFactor = 0.002f;

    public void Initialize()
    {
        EventManager.Subscribe<MouseDownEvent3D>(DragDiceDown);
        EventManager.Subscribe<MouseUpEvent3D>(DragDiceUp);
    }

    void OnDestroy()
    {
        EventManager.UnSubscribe<MouseDownEvent3D>(DragDiceDown);
        EventManager.UnSubscribe<MouseUpEvent3D>(DragDiceUp);
    }

    void Update()
    {

        DragTheDices();
        // Zarlar durduysa ve sonuç daha önce alınmadıysa
        if (!_resultsRecorded && !_dragging && AllDicesStopped())
        {
            UpdateDiceUI();
            _resultsRecorded = true;
        }
    }

    private void UpdateDiceUI()
    {
        for (int i = 0; i < _dices.Count; i++)
        {
            _diceTexts[i].text = _dices[i].GetNumber().ToString();
        }
        EventManager.Raise(new DiceRolled(_dices[0].GetNumber(), _dices[1].GetNumber()));
    }

    private bool AllDicesStopped()
    {
        foreach (Dice dice in _dices)
        {
            if (dice.IsMoving()) return false;
        }
        return true;
    }

    private void DragDiceDown(MouseDownEvent3D data)
    {
        if (data.MouseObject == null) return;
        _lastMouseScreenPos = Input.mousePosition;

        for (int i = 0; i < _dices.Count; i++)
        {
            var rb = _dices[i].GetComponent<Rigidbody>();
            rb.useGravity = false;
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            _dices[i].ReturnStartPosition();
        }

        _dragging = true;
    }

    private void DragTheDices()
    {
        if (!_dragging) return;

        Vector3 currentScreenPos = Input.mousePosition;
        Vector3 screenDelta = currentScreenPos - _lastMouseScreenPos;
        _lastMouseScreenPos = currentScreenPos;

        if (screenDelta.sqrMagnitude > 0.01f)
        {
            Vector3 worldDelta = Camera.main.transform.TransformDirection(new Vector3(screenDelta.x, screenDelta.y, 0f));
            worldDelta.z = 0f;

            _mouseDirection = worldDelta.normalized;
            _mouseSpeed = screenDelta.magnitude;

            Vector3 screenVelocity = screenDelta / Time.deltaTime;
            _mouseVelocity = Camera.main.transform.TransformDirection(new Vector3(screenVelocity.x, screenVelocity.y, 0f));
            _mouseVelocity.z = 0f;
        }

        Vector3 mouseWorld = Camera.main.ScreenToWorldPoint(new Vector3(currentScreenPos.x, currentScreenPos.y, 4f));
        _diceParent.position = Vector3.SmoothDamp(_diceParent.position, mouseWorld, ref _velocity, 0.2f);
    }

    private void DragDiceUp(MouseUpEvent3D data)
    {
        if (!_dragging) return;
        _dragging = false;

        for (int i = 0; i < _dices.Count; i++)
        {
            var rb = _dices[i].GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;

            Vector3 force = _mouseDirection * _mouseSpeed * throwForce
                            + _mouseVelocity * velocityFactor;
            rb.AddForce(force, ForceMode.Impulse);
            rb.AddTorque(Random.insideUnitSphere * 5f, ForceMode.Impulse);
        }
        StartCoroutine(delayIE());
    }
    private IEnumerator delayIE()
    {
        yield return new WaitForSeconds(.1f);
        _resultsRecorded = false; // ✅ yeni fırlatma başladığında reset

    }
}
