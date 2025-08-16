using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DiceHandler : MonoBehaviour
{
    [SerializeField] private List<Dice> _dices;          // Zarlar
    [SerializeField] private List<TMP_Text> _diceTexts;  // Her zar için ayrı TMP_Text

    private bool _resultsRecorded = false;

    void Update()
    {
        if (!_resultsRecorded && AllDicesStopped())
        {
            for (int i = 0; i < _dices.Count; i++)
            {
                int number = _dices[i].GetNumber();
                _diceTexts[i].text = $"{number}";
            }

            _resultsRecorded = true; // Sonuçları bir kere alıyoruz
        }
    }

    private bool AllDicesStopped()
    {
        foreach (Dice dice in _dices)
        {
            if (dice.IsMoving()) return false;
        }
        return true;
    }
}
