using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoutineEventExecutor : MonoBehaviour {

    [SerializeField] RoutineEvent _routineEvent;

    [SerializeField] float _timeCycle = 10f;
    [SerializeField] float _spendTime = 0f;
    float _timeSpend;

    bool _nowExecute;

    private void OnEnable()
    {
        _timeSpend = _spendTime;
        _nowExecute = false;
    }

    public void StartRoutine()
    {
        _timeSpend -= _timeCycle;
        _nowExecute = false;
    }

    private void Update()
    {
        if (_nowExecute) return;
        _timeSpend += Time.deltaTime;

        if (_timeSpend < _timeCycle) return;

        _routineEvent.ExecuteEvent(this);
        _nowExecute = true;

    }
}
