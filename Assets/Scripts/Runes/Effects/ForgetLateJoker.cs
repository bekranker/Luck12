using System;
using UnityEngine;


[CreateAssetMenu(fileName = "ForgetLateJoker", menuName = "Rune Effect - ForgetLateJoker")]
public class ForgetLateJoker : RuneEffect
{
    public override Type EventType => typeof(OnEndOfTheRound);
    public int OnRound;
    public float ChipCount;
    public override void EffectAction(object data)
    {
        // OnEndOfTheRound onEndOfTheRound = (OnEndOfTheRound)data;
        // if (onEndOfTheRound.RoundIndex % 3 != 0) return;
        // onEndOfTheRound.ScoreComponent.AddScore(ChipCount, 1);
        Debug.Log("Hello from ForgetLateJoker");
    }
}
public class SharePutJoker : RuneEffect
{
    public override Type EventType => typeof(DivideEffect);

    public override void EffectAction(object data)
    {
        DivideEffect divideEffect = (DivideEffect)data;

        Debug.Log("Hello from Shae Put Joker");
    }
}
