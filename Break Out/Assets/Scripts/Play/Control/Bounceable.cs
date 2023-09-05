using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounceable : Move
{

    static readonly float value = Mathf.PI - 0.5f;

    [SerializeField] float _power;

    Rigidbody2D _rb;

    public void SetDir(Vector3 dir)
    {
        if (!_rb)
            _rb = GetComponent<Rigidbody2D>();

        _rb.velocity = dir.normalized * _power;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (!_rb)
        {
            _rb = GetComponent<Rigidbody2D>();
            _rb.velocity = _dir * _power;
        }
    }

    protected override void DoMove()
    {
        Vector3 direct;

        if (Mathf.Abs(Mathf.Atan2(_rb.velocity.y, _rb.velocity.x)) < 0.5f || Mathf.Abs(Mathf.Atan2(_rb.velocity.y, _rb.velocity.x)) > value)
        {
            direct = new Vector3(Mathf.Cos(0.5f) * Mathf.Sign(_rb.velocity.x), Mathf.Sin(0.5f) * Mathf.Sign(_rb.velocity.y), 0);
        }
        else
            direct = _rb.velocity;

        SetDir(direct);
    }

}
