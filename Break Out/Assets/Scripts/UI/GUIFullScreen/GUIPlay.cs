using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.ComponentModel.Design;
using ObjectPool;

public class GUIPlay : GUICustomFullScreen {

    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _gameOver;

    [SerializeField] private PhaseController _phaseController;
    [SerializeField] private TextMeshProUGUI _scoreView;

    public virtual void SetScore(int score)
    {
        _scoreView.text = string.Format("<mspace=\"0.45\">{0}</mspace>", score.ToString("000000"));
    }

    protected virtual void Start()
    {
        GameManager.Instance.StartGame();
        GameManager.Instance.SetScoreView(SetScore);
        _phaseController.GameStart();
        SetBudget();
    }

    public virtual void GameOver()
    {
        _gameOver.SetActive(true);
        GameManager.Instance.SetScoreView(null);
    }

    public void Pause()
    {
        _pause.SetActive(true);
        Time.timeScale = 0f;
    }

    public void Continue() {
        _pause.SetActive(false);
        Time.timeScale = 1f;
    }

    public override void Close()
    {
        base.Close();
        Time.timeScale = 1f;
    }

    public virtual void Retry()
    {

    }

}
