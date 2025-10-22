using UnityEngine;
using UnityEngine.Events;

public class Player : Character, IBallTarget
{
    [SerializeField] private UnityEvent _gameStartEvent;

    public void GameStart()
    {
        _gameStartEvent.Invoke();
    }

    protected override void DamageAct()
    {
        base.DamageAct();

        Effect.PlayEffect("Eft_Damaged", transform);
        SoundManager.Instance.PlayEffect("Player_Damaged");
        
    }

    protected override void DieAct()
    {
        GameManager.Instance.Playground.GameOver();
        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Bullet")) return;
        if (!other.gameObject.activeInHierarchy) return;

        GetDamaged(1);

        other.gameObject.SetActive(false);
        Destroy(other.gameObject);
    }

    public void BallCollideAction()
    {
        SoundManager.Instance.PlayEffect("Collide_Player");
    }
    
}
