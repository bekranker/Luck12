using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class RuneHandler : MonoBehaviour, IInitializable
{
    [SerializeField] private List<RuneData> _runesInHand = new();
    [SerializeField] private Rune _prefab;
    [SerializeField] private int _debugRunesCount;
    [SerializeField] private Transform _parent;


    [Inject] private RuneMoveHandler _runeMove;
    private List<Rune> _createdRunes = new();

    public void Initialize()
    {
        StartCoroutine(spawnRunes());
    }
    private IEnumerator spawnRunes()
    {
        for (int i = 0; i < _debugRunesCount; i++)
        {
            Rune createdRune = Instantiate(_prefab, _parent);
            createdRune.Initialize(_runesInHand[i]);
            _runeMove.ToHand(createdRune);
            AddToCreated(createdRune);
            yield return new WaitForSeconds(.1f);
        }
    }
    void OnDestroy()
    {

    }
    public void AddToCreated(Rune rune) => _createdRunes.Add(rune);
}