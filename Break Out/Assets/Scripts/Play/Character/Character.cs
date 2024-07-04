using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] protected Status _stat;
    [SerializeField] private GaugeUI _gaugeUI;


    [SerializeField] private RemoveAct _dieAct;


    protected virtual void OnEnable() {
        if (_gaugeUI == null) {
            return;
        }
        _gaugeUI.InstantiateSet(_stat.GetMaxValue());
        _gaugeUI.SetGauge(_stat.GetHPRatio());
    }

    public void GetDamaged(int amount) {
        _stat.GetDamage(amount);

        if (_gaugeUI)
        {
            _gaugeUI.SetGauge(_stat.GetHPRatio());
        }

        if (_stat.IsAlive())
        {
            DamageAct();
            return;
        }

        DieAct();
    }

    protected virtual void DamageAct() { 
        
    }

    protected virtual void DieAct() {
        _dieAct.DieAct();
    }

    public void PlayEffect(string effectKey)
    {
        Effect.PlayEffect(effectKey, transform);

    }

}
