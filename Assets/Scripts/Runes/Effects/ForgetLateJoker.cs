using UnityEngine;

public class ForgetLateJoker : RuneEffect<DivideEffect>
{

    public override void EffectAction(DivideEffect divideEffect)
    {
        Debug.Log("From Effect Action Function inside ForgetLateJoker Class: " + divideEffect.Number);
    }
}