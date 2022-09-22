using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [SerializeField] readonly float randomCreateItemValue = 0.2f;

    public void Start() {
        EnemyManager.CreateCount++;
    }

    public void Destroy(string killedBy) {
        switch (killedBy) {
            case "Gong":
                EnemyManager.DestroyByGongCount++;
                break;
            default:
                EnemyManager.DestroyByLaserCount++;
                break;
        }

        float percent = Random.Range(0f, 1f);

        if (percent < randomCreateItemValue)
            GameManager.Instance.CreateItem(transform.position);

        Destroy(gameObject);
    }

}
