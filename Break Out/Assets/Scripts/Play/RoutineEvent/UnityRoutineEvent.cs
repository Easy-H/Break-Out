using UnityEngine;
using UnityEngine.Events;

public class UnityRoutineEvent : RoutineEvent {
    [SerializeField] UnityEvent _event;

    public override void ExecuteEvent(RoutineEventExecutor executor)
    {
        _event.Invoke();
    }
}