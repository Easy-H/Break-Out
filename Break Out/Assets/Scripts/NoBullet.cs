using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Ball : MonoBehaviour
{

    //bool beforeCollPlayer = false;

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
        beforeCollPlayer = false;
        */

        if (collision.collider.CompareTag("Enemy")) {

            Destroy(collision.collider.gameObject);
            
            GameManager.instance.addScore(1000);

            GameManager.scoreProduct *= 1.2f;
            return;
            
        }

        if (GameManager.scoreProduct != 0)
            GameManager.scoreProduct = 1;

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
        GameManager.instance.BallCreate();
    }

}
