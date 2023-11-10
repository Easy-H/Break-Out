using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Character {

    [SerializeField] Transform _ballCreatePos;

    [SerializeField] float _maxX = 1;
    [SerializeField] float _minX = -1;

    [SerializeField] int _goalBallCount;
    private int _nowBallCount;
    private int _ballQueueCount;

    [SerializeField] float _ballCreateTime;

    private void Start()
    {
        GameManager.Instance.SetPlayer(this);
        _ballQueueCount = _goalBallCount;
        StartCoroutine(_CreateBall());
    }

    // Update is called once per frame
    void Update()
    {

        float xPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        xPos = Mathf.Clamp(xPos, _minX, _maxX);

        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }

    public void DamageAct()
    {
        if (_ballQueueCount == 0) {
            StartCoroutine(_CreateBall());
        }

        _ballQueueCount++;

        if (!GameManager.Instance.IsPlaying())
        {
            return;
        }

        Effect.PlayEffect("Eft_Damaged", transform);

    }

    IEnumerator _CreateBall() {

        yield return null;

        while (_ballQueueCount > 0)
        {
            Ball newBall = Ball.CreateBall(_ballCreatePos.position, this);

            _nowBallCount++;
            _ballQueueCount--;

            yield return new WaitForSeconds(_ballCreateTime);
        }
    }

    protected override void DieAct()
    {
        base.DieAct();
        Destroy(gameObject);
    }

    public void BallOut() {
        if (--_nowBallCount < _goalBallCount) {
            GetDamaged(1);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            if (!other.gameObject.activeInHierarchy)
                return;
            GetDamaged(1);
            
            other.gameObject.SetActive(false);
            Destroy(other.gameObject);
        }
    }

}
