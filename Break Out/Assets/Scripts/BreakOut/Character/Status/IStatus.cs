using System;

public interface IStatus : IObservable<IStatus>
{

    public int GetMaxValue();

    public void SetHPMax();

    public void AddHP(int amount);
    public void TakeDamage(int amount);

    public float GetHPRatio();

    public bool IsAlive();

}