using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] public Status _stat;

    [SerializeField] private UnityEvent damageEvent;

    [SerializeField] string _dieEffectKey;

    public void GetDamaged(int amount) {
        _stat.GetDamage(amount);

        damageEvent.Invoke();

        if (!_stat.isAlive())
        {
            DieAct();
            return;
        }
    }

    protected virtual void DieAct() {
        Effect.PlayEffect(_dieEffectKey, transform);
    }

}
