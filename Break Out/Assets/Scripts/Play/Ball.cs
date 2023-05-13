using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

    [SerializeField] float _power;

    Rigidbody2D _rb;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Enemy")) {

            Character hit = collision.collider.GetComponent<Character>();
            hit.GetDamaged(1);

            return;
        }

        if (collision.collider.CompareTag("Finish")) {
            GameManager.Instance.BallOut();
            Destroy(gameObject);
            return;
        }

    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        _rb.velocity = _rb.velocity.normalized * _power;
    }

    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _rb.velocity = Vector3.up * _power;

        GameManager.Instance.BallIn();
    }

}
