using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    float score = 0;
    public static float scoreProduct;   // 연속 충돌시 보너스 점수

    float time;
    
    [SerializeField] AudioSource gameOverMusic = null;
    
    int ballCount = 0;                  // 볼의 갯수: 0개 이하로 떨어지면 체력이 1 감소
    static public int coll = 0;                // 못 넘어가는 지점에 닿은 적의 개수, 0이 아니면 적을 생성할 수 없음
    
    public void addScore(int add) {

        score += add * scoreProduct;
        UIManager.instance.ChangeText(0, score.ToString("000000"));
        
        PhaseManager.instance.PhaseSet(score, time);

    }

    public void BallCreate() {
        ballCount++;
    }

    public void Log1() {
        Debug.Log(1);

    }

    public void BallDestroy() {
        if (--ballCount <= 0)
            Player.instance.GetDamage(1);
    }

    public void gameOver() {
        scoreProduct = 0;
        Destroy (GameObject.FindWithTag("Player"));
        UIManager.instance.OpenWindow(0);
        UIManager.instance.OpenWindow(1);
        gameOverMusic.Play();

    }

    private void Awake()
    {
        GL.Clear(true, true, Color.black);
        instance = this;
        time = 0;
        score = 0;
        scoreProduct = 1;
    }

    private void Start()
    {
        GL.Clear(true, true, Color.black);
        coll = 0;
    }

    private void Update()
    {
        time += Time.deltaTime;
        if (Input.GetKey(KeyCode.R))
        {
            SceneManager.LoadScene(1);
        }
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