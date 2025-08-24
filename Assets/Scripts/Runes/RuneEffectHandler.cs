using System.Collections;
using UnityEngine;
using Zenject;

public class RuneEffectHandler : MonoBehaviour, IInitializable
{
    [SerializeField] private float _effectDelay;
    public void Initialize()
    {
        SequentialEventManager.Init(this);
        SequentialEventManager.Subscribe<DivideEffect>(Test);
    }
    void OnDestroy()
    {
        SequentialEventManager.UnSubscribe<DivideEffect>(Test);

    }
    private IEnumerator Test(DivideEffect data)
    {
        yield return new WaitForSeconds(_effectDelay);
        print(data.Number);
    }
}