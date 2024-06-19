using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Character : MonoBehaviour
{
    [SerializeField] protected Status _stat;
    [SerializeField] private GameObject _hpUI;
    private IGaugeUI _gaugeUI;

    [SerializeField] private UnityEvent _damageEvent;
    [SerializeField] private UnityEvent _dieEvent;

    [SerializeField] string _dieEffectKey;


    protected virtual void OnEnable() {
        if (_gaugeUI == null) {
            _gaugeUI = _hpUI.GetComponent<IGaugeUI>();
        }
        _gaugeUI.InstantiateSet(_stat.GetMaxValue());
        _gaugeUI.SetGauge(_stat.GetHPRatio());
    }

    public void GetDamaged(int amount) {
        _stat.GetDamage(amount);
        _damageEvent.Invoke();
        _gaugeUI.SetGauge(_stat.GetHPRatio());

        if (_stat.IsAlive())
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
