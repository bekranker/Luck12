using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Zenject;

public class RuneEffectHandler : MonoBehaviour, IInitializable
{
    [Inject] private RuneVisualizer _runeVisualizer;
    [SerializeField] private float _effectDelay;
    private List<Rune> _createdRunes;
    private Dictionary<RuneEffectBase, Delegate> _subscribers = new();
    void Awake()
    {
        SequentialEventManager.Init(this);
    }
    public void Initialize()
    {
        EventManager.Subscribe<RunesInitialized>(GetCreatedRunes);
    }
    void OnDisable()
    {
        RemoveRunes();
    }
    public void GetCreatedRunes(RunesInitialized data)
    {
        _createdRunes = data.Runes;

        foreach (Rune rune in data.Runes)
        {
            if (rune == null || rune.GetData() == null || rune.GetData().Effect == null)
            {
                Debug.LogError($"Rune veya Effect eksik! Rune: {rune}");
                continue;
            }

            RuneEffectBase effect = rune.GetData().Effect;

            if (effect.EventType == null)
            {
                Debug.LogError($"Effect {effect.name} EventType tanımlı değil!");
                continue;
            }

            Type eventType = effect.EventType;

            var method = typeof(SequentialEventManager)
                .GetMethod("Subscribe", System.Reflection.BindingFlags.Public | System.Reflection.BindingFlags.Static)
                .MakeGenericMethod(eventType);

            // listener sarmalayıcı
            Func<object, IEnumerator> wrapper = (obj) =>
            {
                effect.EffectAction(obj, rune, _runeVisualizer);
                return null;
            };

            var del = Delegate.CreateDelegate(
                typeof(Func<,>).MakeGenericType(eventType, typeof(IEnumerator)),
                wrapper.Target,
                wrapper.Method
            );
            _subscribers[effect] = del;
            method.Invoke(null, new object[] { del });
        }
    }
    public void RemoveRunes()
    {
        foreach (var kvp in _subscribers)
        {
            RuneEffectBase effect = kvp.Key;
            Delegate del = kvp.Value;

            Type eventType = effect.EventType;
            var method = typeof(SequentialEventManager)
                .GetMethod("UnSubscribe", BindingFlags.Public | BindingFlags.Static)
                .MakeGenericMethod(eventType);

            method.Invoke(null, new object[] { del });
        }

        _subscribers.Clear();
    }

}