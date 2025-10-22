using UnityEngine;
using System;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] private UnityEvent _dieEvent;
    [SerializeField] private UnityEvent _damagedEvent;

    [SerializeField] protected Status _status;
    [SerializeField] private StatusUIBase _gaugeUI;
    private IDisposable _cancellation;


    protected virtual void OnEnable()
    {
        if (_gaugeUI == null)
        {
            return;
        }

        _cancellation = _status.Subscribe(_gaugeUI);

    }

    protected void OnDisable()
    {
        _cancellation?.Dispose();
    }

    void OnDestroy()
    {
        _cancellation?.Dispose();
    }

    public void GetDamaged(int amount)
    {
        _status.TakeDamage(amount);

        if (_status.IsAlive())
        {
            DamageAct();
            return;
        }

        DieAct();
    }

    protected virtual void DamageAct()
    {
        _damagedEvent.Invoke();
    }

    protected virtual void DieAct()
    {
        _dieEvent.Invoke();
    }

    public void PlayEffect(string effectKey)
    {
        Effect.PlayEffect(effectKey, transform);

    }

}
