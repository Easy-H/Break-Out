using UnityEngine;
using ObjectPool;

public class Ball : MonoBehaviour {

    private BallOwner _player;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Effect.PlayEffect("Eft_Collide", transform);

        if (collision.collider.CompareTag("Wall")) {
            SoundManager.Instance.PlayEffect("Collide_Wall");
            return;
        }

        IBallTarget target = collision.collider.
            GetComponentInParent<IBallTarget>();

        if (target == null) return;

        target.BallCollideAction();

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet"))
            return;

        other.gameObject.GetComponent<IPoolTarget>().Return();
    }

    public void SetBallOwner(BallOwner owner)
    {
        _player = owner;
    }

    public void BallOut()
    {
        if (!_player) return;

        _player.BallOut();
        StatisticsManager.Instance.BallOut();
    }

}
