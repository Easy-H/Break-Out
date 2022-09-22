using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

    public static GameManager Instance { get; private set; }

    float score = 0;
    public float ScoreProduct { get; set; }     // 연속 충돌시 보너스 점수

    [SerializeField] GameObject[] items;

    float time;

    static public int coll = 0;                // 못 넘어가는 지점에 닿은 적의 개수, 0이 아니면 적을 생성할 수 없음

    public float Score {
        get {
            return score;
        }
        set {
            score += value * ScoreProduct;
            InforUi.Instance.SetScore(score.ToString("000000"));

            PhaseManager.instance.PhaseSet();
        }

    }

    public void CreateItem(Vector3 pos) {
        int random = Random.Range(0, items.Length);

        Instantiate(items[random], pos, Quaternion.identity);
    }

    public float GetTime() {
        return time;
    }


    public void gameOver() {
        ScoreProduct = 0;

        UiManager.Instance.StartAnimation("GameOver");

        Destroy(GameObject.FindWithTag("Player"));

    }

    private void Awake() {
        Instance = this;
        time = 0;
        score = 0;
        ScoreProduct = 1;
    }

    private void Start() {
        coll = 0;
    }

    private void Update() {
        time += Time.deltaTime;
        if (Input.GetKey(KeyCode.R)) {
            SceneManager.LoadScene(1);
        }
    }

}