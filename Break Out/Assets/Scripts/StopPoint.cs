using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopPoint : MonoBehaviour
{
    private void OnTriggerEnter2D (Collider2D collision)
    {
        if (collision.CompareTag("Enemy")) {
            GameManager.coll++;
            collision.gameObject.GetComponent<Enemy>().touched = true;
        }
    }
    


}
