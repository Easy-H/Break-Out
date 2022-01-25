using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Break : MonoBehaviour
{

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Bullet"))
            Destroy(collision.gameObject);
        else if (collision.collider.CompareTag("Ball"))
        {
            GameManager.instance.BallDestroy();
            Destroy(collision.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Bullet"))
            Destroy(collision.gameObject);
    }
}
