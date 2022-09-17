using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gong : MonoBehaviour
{
    Rigidbody2D rb;

    [SerializeField] Vector3 direct;

    float value;
    float speed;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        /*
        if (collision.collider.CompareTag("Player"))
        {
            if (!beforeCollPlayer)
            {
                Skill.AddSkillPoint();
            }
            beforeCollPlayer = true;
            if (GameManager.scoreProduct != 0)
                GameManager.scoreProduct = 1;
            return;
        }
        */

        if (collision.collider.CompareTag("Enemy"))
        {
            collision.collider.GetComponent<Enemy>().Destroy("Gong");

            GameManager.Instance.Score = 1000;
            GameManager.Instance.ScoreProduct *= 1.2f;
            return;

        }

        if (GameManager.Instance.ScoreProduct != 0)
            GameManager.Instance.ScoreProduct = 1;

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
        {
            Destroy(collision.gameObject);
            return;

        }
    }

    private void Start()
    {
        GongManager.Instance.NowBallCount++;
        value = Mathf.PI - 0.5f;
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = direct;
        speed = direct.magnitude;

    }

    private void Update() {
        
        if (Mathf.Abs(Mathf.Atan2(rb.velocity.y, rb.velocity.x)) < 0.5f || Mathf.Abs(Mathf.Atan2(rb.velocity.y, rb.velocity.x)) > value)
        {
            direct = new Vector3(Mathf.Cos(0.5f) * Mathf.Sign(rb.velocity.x), Mathf.Sin(0.5f) * Mathf.Sign(rb.velocity.y), 0).normalized;
        }
        else
            direct = rb.velocity.normalized;

        rb.velocity = direct * speed;
    }

}
