using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounceable : MonoBehaviour
{

    static readonly float value = Mathf.PI - 0.5f;

    [SerializeField] float _power;

    Rigidbody2D _rb;

    private void OnCollisionExit2D(Collision2D collision)
    {
        Vector3 direct;

        if (Mathf.Abs(Mathf.Atan2(_rb.velocity.y, _rb.velocity.x)) < 0.5f || Mathf.Abs(Mathf.Atan2(_rb.velocity.y, _rb.velocity.x)) > value)
        {
            direct = new Vector3(Mathf.Cos(0.5f) * Mathf.Sign(_rb.velocity.x), Mathf.Sin(0.5f) * Mathf.Sign(_rb.velocity.y), 0).normalized;
        }
        else
            direct = _rb.velocity.normalized;

        _rb.velocity = direct * _power;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Vector3.up * _power;

        GameManager.Instance.BallIn();
    }

}
