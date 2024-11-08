using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Character, IBallTarget {

    [SerializeField] Transform _ballCreatePos;

    [SerializeField] Vector2 _min;
    [SerializeField] Vector2 _max;
    [SerializeField] float _up;

    [SerializeField] int _goalBallCount;
    private int _nowBallCount;
    private int _ballQueueCount;

    [SerializeField] float _ballCreateTime;
    [SerializeField] int _damageRatio = 1;

    // Update is called once per frame
    void Update()
    {
        Vector2 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        SetPos(pos.x, pos.y + _up);

    }

    public void SetPos(float xPos, float yPos)
    {
        transform.position = new Vector3(Mathf.Clamp(xPos, _min.x, _max.x), Mathf.Clamp(yPos, _min.y, _max.y), transform.position.z);

    }

    public void GameStart(int goalBallCount = 1, int damageRatio = 1) {
        _goalBallCount = goalBallCount;
        _damageRatio = damageRatio;

        _nowBallCount = 0;
        _ballQueueCount = _goalBallCount;
        StartCoroutine(_CreateBall());
        SoundManager.Instance.PlayEffect("Start");
    }

    public void CreateBall()
    {
        if (_ballQueueCount == 0)
        {
            StartCoroutine(_CreateBall());
        }

        _ballQueueCount++;

    }

    protected override void DamageAct()
    {

        Effect.PlayEffect("Eft_Damaged", transform);
        SoundManager.Instance.PlayEffect("Player_Damaged");
        CreateBall();

    }

    IEnumerator _CreateBall() {

        yield return null;

        while (_ballQueueCount > 0)
        {
            Ball newBall = Ball.CreateBall(_ballCreatePos.position, this);
            StatisticsManager.Instance.BallCreate();

            _nowBallCount++;
            _ballQueueCount--;

            yield return new WaitForSeconds(_ballCreateTime);
        }
    }

    protected override void DieAct()
    {
        base.DieAct();
        GameManager.Instance.Playground.GameOver();
        Destroy(gameObject);
    }

    public void BallOut() {
        if (--_nowBallCount >= _goalBallCount) {
            return;
        }
        GetDamaged(1 * _damageRatio);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet")) return;
        if (!other.gameObject.activeInHierarchy) return;

        GetDamaged(1 * _damageRatio);

        other.gameObject.SetActive(false);
        Destroy(other.gameObject);
    }

    public void BallCollideAction()
    {
        SoundManager.Instance.PlayEffect("Collide_Player");
    }
}
