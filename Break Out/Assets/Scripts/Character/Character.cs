using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] public Status _stat;

    [SerializeField] private UnityEvent _damageEvent;
    [SerializeField] private UnityEvent _dieEvent;

    [SerializeField] string _dieEffectKey;

    public void GetDamaged(int amount) {
        _stat.GetDamage(amount);

        _damageEvent.Invoke();

        if (!_stat.isAlive())
        {
            return;
        }
        _dieEvent.Invoke();
        DieAct();
    }

    protected virtual void DieAct() {
    }

    public void PlayEffect(string effectKey)
    {
        Effect.PlayEffect(effectKey, transform);

    }

}
