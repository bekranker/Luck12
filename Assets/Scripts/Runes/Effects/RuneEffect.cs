using System;
using UnityEngine;

[System.Serializable]
public abstract class RuneEffect<T> : RuneEffectBase
{
    public override Type EventType => typeof(T);
    public abstract void EffectAction(T data, Rune rune, RuneVisualizer runeVisualizer);

    public override void EffectAction(object data, Rune rune, RuneVisualizer runeVisualizer)
    {
        Debug.Log($"{data == null}");

        EffectAction((T)data, rune, runeVisualizer);

    }
}

[System.Serializable]
public abstract class RuneEffectBase : ScriptableObject
{
    public abstract Type EventType { get; }
    public abstract void EffectAction(object data, Rune rune, RuneVisualizer runeVisualizer);
}