using System;
using System.Collections;
using System.Collections.Generic;

public class SequentialGameEvent<T>
{
    private List<Func<T, IEnumerator>> _subs = new();

    public void Subscribe(Func<T, IEnumerator> subscriber)
    {
        if (!_subs.Contains(subscriber))
            _subs.Add(subscriber);
    }

    public void Remove(Func<T, IEnumerator> subscriber)
    {
        if (_subs.Contains(subscriber))
            _subs.Remove(subscriber);
    }

    public IEnumerator Raise(T eventData)
    {
        foreach (var subscriber in _subs)
        {
            // subscriber bitene kadar bekle
            yield return subscriber.Invoke(eventData);
        }
    }
}
