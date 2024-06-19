using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounceable : Move
{
    static readonly float valueMin = 0.5f;
    static readonly float valueMax = Mathf.PI - valueMin;

    Rigidbody2D _rb;

    public void SetDir(Vector2 dir)
    {
        if (!_rb)
            _rb = GetComponent<Rigidbody2D>();

        _rb.velocity = dir.normalized * _power;
    }

    private void OnEnable()
    {
        SetDir(_defaultDir);
    }

    protected override void DoMove()
    {

        if (Mathf.Abs(Mathf.Atan2(_rb.velocity.y, _rb.velocity.x)) > valueMin
            && Mathf.Abs(Mathf.Atan2(_rb.velocity.y, _rb.velocity.x)) < valueMax)
        {
            SetDir(_rb.velocity);
            return;
        }

        SetDir(new Vector2(Mathf.Cos(0.5f) * Mathf.Sign(_rb.velocity.x), Mathf.Sin(0.5f) * Mathf.Sign(_rb.velocity.y)));

    }

}
