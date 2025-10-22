using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using ObjectPool;

public class BallOwner : MonoBehaviour
{
    [SerializeField] private UnityEvent _ballOutEvent;
    [SerializeField] private Transform _ballCreatePos;
    [SerializeField] private int _goalBallCount;
    [SerializeField] float _ballCreateTime;

    private int _nowBallCount;
    private int _ballQueueCount;

    public void CreateBall()
    {
        if (_ballQueueCount == 0)
        {
            StartCoroutine(_CreateBall());
        }

        _ballQueueCount++;

    }

    private IEnumerator _CreateBall()
    {

        yield return null;

        while (_ballQueueCount > 0)
        {
            Transform trBall = ObjectPoolManager.Instance.GetGameObject("Ball").transform;
            trBall.transform.position = _ballCreatePos.position;

            Ball retval = trBall.GetComponent<Ball>();
            retval.SetBallOwner(this);

            StatisticsManager.Instance.BallCreate();

            _nowBallCount++;
            _ballQueueCount--;

            yield return new WaitForSeconds(_ballCreateTime);
        }
    }

    public void BallOut()
    {
        if (--_nowBallCount >= _goalBallCount)
        {
            return;
        }
        _ballOutEvent?.Invoke();
    }
    
}