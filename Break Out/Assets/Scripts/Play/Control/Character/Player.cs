using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    [SerializeField] Transform _ballCreatePos;
    [SerializeField] GUIPlay _play;

    [SerializeField] GameObject _ball;

    [SerializeField] float _maxX = 1;
    [SerializeField] float _minX = -1;

    private void Start()
    {
        InstantiateHP();
        GameManager.Instance.SetPlayer(this);
    }

    // Update is called once per frame
    void Update()
    {

        float xPos = Camera.main.ScreenToWorldPoint(Input.mousePosition).x;
        xPos = Mathf.Clamp(xPos, _minX, _maxX);

        transform.position = new Vector3(xPos, transform.position.y, transform.position.z);
    }

    protected override void DamageAct() {

        Ball.CreateBall(_ballCreatePos.position).GetComponent<Bounceable>().SetDir(Vector3.up);
        

        if (!GameManager.Instance.IsPlaying())
        {
            return;
        }

        base.DamageAct();

    }

    protected override void DieAct()
    {
        base.DieAct();
        _play.GameOver();
        Destroy(gameObject);
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
