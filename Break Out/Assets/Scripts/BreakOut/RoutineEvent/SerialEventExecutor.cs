using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SerialEventExecutor : MonoBehaviour
{

    [SerializeField] private UnityEvent _event;
    [SerializeField] private float _serialTerm = 0f;
    [SerializeField] private int _serialCount = 1;

    private int _leftCreateCount;

    public void ExecuteEvent()
    {
        _leftCreateCount = _serialCount;

        StartCoroutine(_ExecuteEvent());
    }

    private IEnumerator _ExecuteEvent()
    {
        while (_leftCreateCount > 0)
        {

            _leftCreateCount--;

            _event.Invoke();

            yield return new WaitForSeconds(_serialTerm);
        }
    }

}