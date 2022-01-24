using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour
{
    [SerializeField] GameObject[] created = null;

    [SerializeField] Transform[] createPos = null;

    [SerializeField] float createTime = 2;
    [SerializeField] float spendTime = 0;

    [SerializeField] int goalCreateCount = 1;

    [SerializeField] bool createEnemy = false;

    private void Update()
    {
        spendTime += Time.deltaTime * GameManager.level;

        if (spendTime >= createTime)
        {
            spendTime -= createTime;
            int createCount = 0;

            if (createEnemy && (!GameManager.instance.CheckCreateEnemy() || GameManager.coll))
                return;

            for (int i = 0; i < createPos. Length && createCount < goalCreateCount; i++) {

                if (goalCreateCount - createCount == createPos.Length - i|| Random.Range(0, createPos.Length) < goalCreateCount) {
                    Instantiate(created[Random.Range(0, created.Length)], createPos[i].position, Quaternion.identity);
                    createCount++;

                    if (createEnemy)
                        GameManager.enemyCount++;

                }

            }

        }
    }
}
