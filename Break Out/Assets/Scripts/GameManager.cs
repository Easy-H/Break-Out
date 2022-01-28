using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    
    [SerializeField] float log = 2;

    float score = 0;
    public static float scoreProduct;   // 연속 충돌시 보너스 점수
    
    public static float level;          // 레벨이 올라갈수록 게임의 진행도가 빨라짐

    float time;

    public static int enemyCount = 0;

    public static int phase = 0;
    public static bool coll = false;
    [SerializeField] float phaseFactor = 20;
    [SerializeField] int[] phaseChangeScore = null;
    [SerializeField] int[] phaseMaxEnemy = null;
    [SerializeField] GameObject[] phaseCreator = null;


    int ballCount = 0;                  // 볼의 갯수: 0개 이하로 떨어지면 체력이 1 감소

    public bool CheckCreateEnemy() {

        if (phaseMaxEnemy[phase] > enemyCount)
            return true;
        else
            return false;
    }

    public void addScore(int add) {

        score += add * scoreProduct;
        UIManager.instance.ChangeText(0, score.ToString("000000"));

        float timeScale = score * 0.001f;

        if (timeScale == 0)
            return;

        level = Mathf.Log(time + log - phase * phaseFactor, log);

        if (score >= phaseChangeScore[phase]) {
            phaseCreator[phase++].SetActive(false);
            phaseCreator[phase].SetActive(true);
        }

    }

    public void BallCreate() {
        ballCount++;
    }

    public void BallDestroy() {
        if (--ballCount <= 0)
            Player.GetDamage(1);
    }

    public void gameOver() {
        scoreProduct = 0;
        Destroy (GameObject.FindWithTag("Player"));
        UIManager.instance.OpenWindow(0);
        UIManager.instance.OpenWindow(1);

    }

    private void Awake()
    {
        GL.Clear(true, true, Color.black);
        instance = this;
        time = 0;
        level = 1;
        score = 0;
        scoreProduct = 1;
        phase = 0;
        enemyCount = 0;
    }

    private void Start()
    {
        GL.Clear(true, true, Color.black);
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
    }

    private void FixedUpdate()
    {

        coll = false;
    }

    public void gotoScene(int idxScene) {
        SceneManager.LoadScene(idxScene);
    }

    public void gameStop() {
        Time.timeScale = 0;

    }

    public void gameStart() {
        Time.timeScale = 1;

    }
    void OnPreCull() => GL.Clear(true, true, Color.black);

}