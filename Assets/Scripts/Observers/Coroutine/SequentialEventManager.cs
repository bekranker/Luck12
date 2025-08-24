using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SequentialEventManager
{
    private static Dictionary<Type, object> _eventTable = new();
    private static MonoBehaviour _runner;

    // İlk sahnede bir defa çağır (GameManager vs. içinden)
    public static void Init(MonoBehaviour runner)
    {
        _runner = runner;
    }

    public static SequentialGameEvent<T> GetEvent<T>()
    {
        var type = typeof(T);

        if (!_eventTable.ContainsKey(type))
            _eventTable[type] = new SequentialGameEvent<T>();

        return (SequentialGameEvent<T>)_eventTable[type];
    }

    public static void Subscribe<T>(Func<T, IEnumerator> listener) => GetEvent<T>().Subscribe(listener);
    public static void UnSubscribe<T>(Func<T, IEnumerator> listener) => GetEvent<T>().Remove(listener);

    public static void Raise<T>(T eventData)
    {
        if (_runner == null)
        {
            Debug.LogError("SequentialEventManager: Runner ayarlanmamış! Init() çağır.");
            return;
        }
        _runner.StartCoroutine(GetEvent<T>().Raise(eventData));
    }
}
