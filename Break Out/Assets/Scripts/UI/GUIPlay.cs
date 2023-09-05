using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.ComponentModel.Design;

public class GUIPlay : GUICustomFullScreen {

    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _gameOver;

    [SerializeField] private TextMeshProUGUI _scoreView;

    public virtual void SetScore(int score)
    {
        _scoreView.text = "<mspace=\"0.45\">" + score.ToString("000000") + "</mspace>";
    }

    protected virtual void Start()
    {
        GameManager.Instance.StartGame();
        GameManager.Instance.SetScoreView(SetScore);
    }

    public void GameOver()
    {
        _gameOver.SetActive(true);
        GameManager.Instance.SetScoreView(null);
    }

    public void Pause()
    {
        _pause.SetActive(true);
    }

    public virtual void Retry() {

    }

}
