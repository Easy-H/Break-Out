using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Character : MonoBehaviour
{
    [SerializeField] Status _stat;
    [SerializeField] HpBar _hpBar;

    [SerializeField] string _dieEffectKey;

    public void GetDamaged(int amount) {
        _stat.GetDamage(amount);
        ShowHP();

        if (!_stat.isAlive())
        {
            DieAct();
            return;
        }
        DamageAct();
    }

    protected virtual void DamageAct()
    {
    }

    protected void InstantiateHP()
    {
        _hpBar.InstantiateSet(_stat._nowHP, _stat._maxHP);

    }

    protected void ShowHP()
    {
        _hpBar.SetHP(_stat._nowHP, _stat._maxHP);

    }

    protected virtual void DieAct() {
        Effect.PlayEffect(_dieEffectKey, transform);
    }

}
