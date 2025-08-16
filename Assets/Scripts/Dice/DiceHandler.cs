using System.Collections.Generic;
using UnityEngine;

public class DiceHandler : MonoBehaviour
{
    [SerializeField] private List<Dice> _dices;

    private List<int> _results = new(); // Zar sonuçları burada saklanacak
    private bool _resultsRecorded = false;

    void Update()
    {
        // Eğer tüm zarlar durduysa ve sonuçlar henüz alınmadıysa
        if (!_resultsRecorded && AllDicesStopped())
        {
            _results.Clear();

            foreach (Dice dice in _dices)
            {
                int number = dice.GetNumber();
                _results.Add(number);
                Debug.Log($"{dice.name} rolled: {number}");
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

    public List<int> GetResults() => _results;
}
