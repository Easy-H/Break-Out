using UnityEngine;
using System;

public abstract class StatusUIBase : MonoBehaviour, IObserver<IStatus>
{
    private bool _isSetting = false;

    public void OnCompleted()
    {

    }

    public void OnError(Exception error)
    {

    }

    public void OnNext(IStatus value)
    {
        if (!_isSetting)
        {
            InstantiateSet(value.GetMaxValue());
            _isSetting = true;
        }

        SetGauge(value.GetHPRatio());
    }

    protected abstract void InstantiateSet(int max);

    protected abstract void SetGauge(float amount);
    
}
