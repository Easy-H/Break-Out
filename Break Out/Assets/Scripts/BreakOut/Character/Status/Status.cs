using UnityEngine;
using System;
using System.Collections.Generic;
using EasyH;

public class Status : MonoBehaviour, IStatus
{

    [SerializeField] private int _maxHP;
    [SerializeField] private int _nowHP;

    private ISet<IObserver<IStatus>> _observers
        = new HashSet<IObserver<IStatus>>();

    public IDisposable Subscribe(IObserver<IStatus> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);

            observer.OnNext(this);
        }

        return new Unsubscriber<IStatus>(_observers, observer);
    }

    private void Notify()
    {
        foreach (var o in _observers)
        {
            o.OnNext(this);
        }
    }

    public int GetMaxValue()
    {
        return _maxHP;
    }

    public void SetHPMax()
    {
        _nowHP = _maxHP;
        Notify();
    }

    public void AddHP(int amount)
    {
        _nowHP += amount;
        if (_nowHP > _maxHP) _nowHP = _maxHP;
        Notify();
    }

    public void TakeDamage(int amount)
    {
        _nowHP -= amount;
        Notify();
    }

    public float GetHPRatio()
    {
        return (float)_nowHP / _maxHP;
    }

    public bool IsAlive() { return _nowHP > 0; }
}
