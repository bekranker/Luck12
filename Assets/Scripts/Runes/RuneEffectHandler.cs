using System;
using System.Collections;
using System.Collections.Generic;
using ModestTree;
using UnityEngine;
using Zenject;

public class RuneEffectHandler : MonoBehaviour, IInitializable
{

    [SerializeField] private float _effectDelay;
    private List<Rune> _createdRunes;
    public void Initialize()
    {
        SequentialEventManager.Init(this);
        SequentialEventManager.Subscribe<DivideEffect>(Test);
        EventManager.Subscribe<RunesInitialized>(GetCreatedRunes);
    }
    void OnDestroy()
    {
        SequentialEventManager.UnSubscribe<DivideEffect>(Test);
        EventManager.UnSubscribe<RunesInitialized>(GetCreatedRunes);
    }
    public void GetCreatedRunes(RunesInitialized data)
    {
        _createdRunes = data.Runes;
        foreach (Rune rune in data.Runes)
        {
            Type type = rune.GetData().Effect.GetType();
            // SequentialEventManager.Subscribe<>(rune.GetData().Effect);
        }
    }
    private IEnumerator Test(DivideEffect data)
    {
        yield return new WaitForSeconds(_effectDelay);
        print(data.Number);
    }
}