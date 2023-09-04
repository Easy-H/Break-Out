using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Effect effect = ObjectPool.Instance.GetGameObject("CollideEffect").GetComponent<Effect>();
        effect.On(collision.contacts[0].point);
        effect.transform.SetParent(transform.parent);

        if (collision.collider.CompareTag("Enemy")) {

            Character hit = collision.collider.GetComponent<Character>();
            hit.GetDamaged(1);

            return;
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            other.gameObject.GetComponent<PoolTarget>().Return();
            return;
        }
    }

    public static Ball CreateBall(Vector3 pos) {

        Transform trBall = ObjectPool.Instance.GetGameObject("Ball").transform;
        trBall.transform.position = pos;

        GameManager.Instance.BallIn();

        return trBall.GetComponent<Ball>();
    }

    public void BallOut()
    {
        GameManager.Instance.BallOut();
    }

}
