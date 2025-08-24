using System;
using UnityEngine;

[System.Serializable]
public abstract class RuneEffect : ScriptableObject
{
    public abstract Type EventType { get; }
    public abstract void EffectAction(object data);
}