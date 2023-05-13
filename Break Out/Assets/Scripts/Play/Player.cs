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
        ShowHP();
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

        base.DamageAct();

        Instantiate(_ball, _ballCreatePos.position, Quaternion.identity);
    }

    protected override void DieAct()
    {
        _play.GameOver();
        Destroy(gameObject);
    }

}
