using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPoint : MonoBehaviour
{
    [SerializeField] Boomber boom = null;

    private void OnTriggerEnter2D (Collider2D collision)
    {
        /*
        if (collision.CompareTag("Enemy")) {
            GameManager.coll++;
            collision.gameObject.GetComponent<Enemy>().touched = true;
        }
        */
        if (collision.CompareTag("Enemy"))
        {
            Instantiate(boom, collision.gameObject.transform.position, Quaternion.identity);
            GameManager.enemyCount--;
            Destroy(collision.gameObject);
        }

    }
    


}
