using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIPlay : GUICustomFullScreen {

    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _gameOver;

    [SerializeField] private TextMeshProUGUI _scoreView;

    public virtual void SetScore(int score)
    {
        _scoreView.text = "<mspace=\"0.45\">" + score.ToString("000000") + "</mspace>";
    }

    protected void Start()
    {
        GameManager.Instance.SetScoreView(SetScore);
    }

    public void GameOver()
    {
        _gameOver.SetActive(true);
    }

    public void Pause()
    {
        _pause.SetActive(true);
    }

}
