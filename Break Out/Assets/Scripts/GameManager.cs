using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum GameState { 
    pause, play, clear
}

public delegate void ScoreAct(int score);

public class GameManager : Singleton<GameManager>
{
    GameState _state;

    ScoreAct _actor;

    public int Score { get; private set; }
    public int BossKillCount { get; private set; }

    public bool IsPlaying()
    {
        return _state == GameState.play;

    }

    public void AddScore(int amount) {
        Score += amount;
        if (_actor == null)
            return;
        _actor.Invoke(Score);
    }

    public void SetScoreView(ScoreAct act) {
        _actor = act;
    }

    public void StartGame()
    {
        _state = GameState.play;
        Score = 0;
    }
    public void EndGame()
    {
        _state = GameState.clear;
    }

}
