using System;

public class SharePutJoker : RuneEffect<DivideEffect>
{
    public override Type EventType => typeof(DivideEffect);

    public override void EffectAction(DivideEffect data, Rune rune, RuneVisualizer runeVisualizer)
    {
        // DivideEffect divideEffect = (DivideEffect)data;

        // Debug.Log("Hello from Shae Put Joker: " + divideEffect.Number.ToString());
    }
}
