using UnityEngine;
using UnityEngine.Events;

public class BallTarget : MonoBehaviour, IBallTarget
{
    [SerializeField] private UnityEvent _event;

    public void BallCollideAction()
    {
        _event.Invoke();
    }
}
