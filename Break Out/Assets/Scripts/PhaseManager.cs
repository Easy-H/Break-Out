using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager instance;

    [SerializeField] float log = 2;

    public static float level;          // 레벨이 올라갈수록 게임의 진행도가 빨라짐

    int enemyCount = 0;

    public static int phase = 0;
    [SerializeField] float phaseFactor = 20;
    [SerializeField] Phase[] phases = null;

    public bool CheckCreateEnemy() {
        if (phases[phase].maxEnemy > enemyCount)
            return true;
        else
            return false;
    }

    public void EnemyCreated() {
        enemyCount++;
    }

    public void PhaseSet(float score, float time)
    {
        if (phases[phase].PhaseEndCheck(score))
        {
            phases[phase++].gameObject.SetActive(false);
            phases[phase].gameObject.SetActive(true);
        }

        level = Mathf.Log(time - phase * phaseFactor, log);


    }

    public void EnemyOut() {
        enemyCount--;
        if (phases[phase].PhaseEndCheck())
        {
            phases[phase++].gameObject.SetActive(false);
            phases[phase].gameObject.SetActive(true);
        }

    }

    private void Awake()
    {
        instance = this;
        level = 1;
        phase = 0;
        enemyCount = 0;
    }
  

}