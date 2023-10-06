using Newtonsoft.Json.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour {

    Player _player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Effect.PlayEffect("Eft_Collide", transform);

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

    public static Ball CreateBall(Vector3 pos, Player player) {

        Transform trBall = ObjectPool.Instance.GetGameObject("Ball").transform;
        trBall.transform.position = pos;

        Ball retval = trBall.GetComponent<Ball>();
        retval._player = player;

        return retval;
    }

    public void BallOut()
    {
        if (_player)
            _player.BallOut();

        Effect.PlayEffect("Eft_BallOut", transform.position + Vector3.up, transform.parent);

    }

}
