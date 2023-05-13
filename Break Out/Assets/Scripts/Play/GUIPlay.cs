using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GUIPlay : GUICustomFullScreen {

    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _gameOver;

    [SerializeField] private TMPro.TextMeshPro _scoreView;

    public void SetScore(int score) { 
        _scoreView.text = "<mspace=\"4\">" + score.ToString("000000") + "</mspace>";
    }

    protected void Start()
    {
        GameManager.Instance.SetScoreView(SetScore);
    }

    public void GameOver() {
        _gameOver.SetActive(true);
    }

    public void Pause()
    {

    }

}
