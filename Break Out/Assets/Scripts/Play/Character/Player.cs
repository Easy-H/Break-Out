using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Character, IBallTarget {


    [SerializeField] Transform _ballCreatePos;

    [SerializeField] float _maxX = 1;
    [SerializeField] float _minX = -1;

    [SerializeField] private UnityEvent _allBallOutEvent;

    [SerializeField] int _goalBallCount;
    private int _nowBallCount;
    private int _ballQueueCount;

    [SerializeField] float _ballCreateTime;

    [SerializeField] GameObject[] models;

    protected override void OnEnable()
    {

        base.OnEnable();
        for (int i = 0; i < models.Length; i++) {
            models[i].SetActive(PlayerManager.Instance.NowKey == i);
        }
        SoundManager.Instance.PlayEffect("Start");

    }

    private void Start()
    {
        _nowBallCount = 0;
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

    public void CreateBall()
    {
        if (_ballQueueCount == 0)
        {
            StartCoroutine(_CreateBall());
        }

        _ballQueueCount++;

    }

    public void DamageAct()
    {
        if (!GameManager.Instance.IsPlaying())
        {
            return;
        }

        Effect.PlayEffect("Eft_Damaged", transform);
        SoundManager.Instance.PlayEffect("Player_Damaged");

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
        Destroy(gameObject);
    }

    public void BallOut() {
        if (--_nowBallCount < _goalBallCount) {
            _allBallOutEvent.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet")) return;
        if (!other.gameObject.activeInHierarchy) return;

        GetDamaged(1);

        other.gameObject.SetActive(false);
        Destroy(other.gameObject);
    }

    public void BallCollideAction()
    {
        Debug.Log("Collide_Player");
        SoundManager.Instance.PlayEffect("Collide_Player");
    }
}
