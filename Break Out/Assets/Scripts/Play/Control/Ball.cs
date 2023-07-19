using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    [SerializeField] Effect _collideEffect;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _collideEffect.On(collision.contacts[0].point);

        if (collision.collider.CompareTag("Enemy")) {

            Character hit = collision.collider.GetComponent<Character>();
            hit.GetDamaged(1);

            return;
        }

        if (collision.collider.CompareTag("Finish")) {
            GameManager.Instance.BallOut();
            Destroy(_collideEffect.gameObject);
            Destroy(gameObject);
            return;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(other.gameObject);
            return;
        }
    }

    private void Start()
    {
        _collideEffect.transform.SetParent(null);
    }

}
