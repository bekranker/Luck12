using System;
using UnityEngine;


[CreateAssetMenu(fileName = "ForgetLateJoker", menuName = "Rune Effect - ForgetLateJoker")]
public class ForgetLateJoker : RuneEffect<OnEndOfTheRound>
{
    public override Type EventType => typeof(OnEndOfTheRound);
    public int OnRound;
    public float ChipCount;
    public override void EffectAction(OnEndOfTheRound data, Rune rune, RuneVisualizer runeVisualizer)
    {

        Debug.Log("Hello from ForgetLateJoker");
        if (data.RoundIndex % 3 == 0) return;
        runeVisualizer.PunchScale(rune);
        data.ScoreComponent.AddScore(ChipCount, 1);
    }

}
