using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RoutineEventExecutor : MonoBehaviour {

    [SerializeField] UnityEvent _routineEvent;

    [SerializeField] float _timeCycle = 10f;
    [SerializeField] float _spendTime = 0f;

    private void Update()
    {
        _spendTime += Time.deltaTime;

        if (_spendTime < _timeCycle) return;

        _spendTime -= _timeCycle;
        _routineEvent.Invoke();

    }
}
