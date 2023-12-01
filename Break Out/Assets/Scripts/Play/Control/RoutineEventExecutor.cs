using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoutineEventExecutor : MonoBehaviour {

    [SerializeField] UnityEvent _routineEvent;

    [SerializeField] float _timeCycle = 10f;
    [SerializeField] float _spendTime = 0f;
    float _timeSpend;

    private void OnEnable()
    {
        _timeSpend = _spendTime;
    }

    private void Update()
    {
        _timeSpend += Time.deltaTime;

        if (_timeSpend < _timeCycle) return;

        _timeSpend -= _timeCycle;
        _routineEvent.Invoke();

    }
}
