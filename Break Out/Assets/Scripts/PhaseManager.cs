using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhaseManager : MonoBehaviour
{
    public static PhaseManager instance;

    [SerializeField] float log = 2;

    public static float level;          // 레벨이 올라갈수록 게임의 진행도가 빨라짐

    [SerializeField] int enemyCount = 0;

    public static int phase = 0;
    [SerializeField] float phaseFactor = 20;
    [SerializeField] Phase[] phases = null;

    public bool CheckCreateEnemy() {
        if (phases[phase].maxEnemy > EnemyManager.NowEnemyCount())
            return true;
        else
            return false;
    }
    
    public void PhaseSet()
    {
        if (phases[phase].EndCheck()) {
            phases[phase++].gameObject.SetActive(false);
            phases[phase].gameObject.SetActive(true);
        }
        
    }

    public void PhaseEnd() {
        phases[phase++].gameObject.SetActive(false);
        phases[phase].gameObject.SetActive(true);

        level = Mathf.Log(GameManager.Instance.GetTime() - phase * phaseFactor + log, log);
    }

    private void Awake()
    {
        instance = this;
        level = 1;
        phase = 0;
        enemyCount = 0;
    }
  

}