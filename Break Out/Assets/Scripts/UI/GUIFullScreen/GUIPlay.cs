using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.ComponentModel.Design;
using ObjectPool;
using UnityEngine.SocialPlatforms.Impl;

public class GUIPlay : GUICustomFullScreen, IPlayground {

    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _gameOver;

    [SerializeField] private PhaseController _phaseController;
    [SerializeField] private TextMeshProUGUI _scoreView;

    [SerializeField] private Player _player;

    protected int Score;

    bool _isPlaying;

    public virtual void AddScore(int amount)
    {
        if (!_isPlaying) return;

        Score += amount;
        _scoreView.text = string.Format("<mspace=\"0.45\">{0}</mspace>", Score.ToString("000000"));

    }

    public override void SetOn()
    {
        base.SetOn();
        _isPlaying = true;
        GameManager.Instance.SetPlayground(this);
        _phaseController.GameStart();
        _player.GameStart();
    }

    public virtual void GameOver()
    {
        _gameOver.SetActive(true);
        _isPlaying = false;
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
