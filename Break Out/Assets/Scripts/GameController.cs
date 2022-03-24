using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public UIManager ui;

    int score = 0;
    int scoreProduct = 1;

    [SerializeField] int breakLife = 3;

    public void Score(int add) {

        score += add * scoreProduct;
        ui.ChangeText(0, score.ToString("000000"));

        float timeScale = score * 0.001f;

        if (timeScale == 0)
            return;

        timeScale = -1 / timeScale + 2;

        Time.timeScale = timeScale;
    }

    public void BreakHit() {
    }
    
}
