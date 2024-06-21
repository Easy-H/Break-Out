using UnityEngine;

public abstract class RoutineEvent : MonoBehaviour {
    abstract public void ExecuteEvent(RoutineEventExecutor executor);
}