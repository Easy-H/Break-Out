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

    Player _player;

    public int Score { get; private set; }
    public int BossKillCount { get; private set; }
    public int KillCount { get; private set; }

    public int BallCount { get; private set;  }

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

    public void BossKill()
    {
        BossKillCount++;

    }
    
    public void EnemyKill() {
        KillCount++;
    }

    public void SetScoreView(ScoreAct act) {
        _actor = act;
    }

    public void SetPlayer(Player player) {
        _player = player;
    }

    public void BallIn() {
        BallCount++;
    }

    public void BallOut()
    {

        if (_player == null)
            return;

        if (--BallCount >= 0)
            return;

        if (_state == GameState.clear) {
            _player.GetDamaged(0);
            return;
        }

        _player.GetDamaged(1);

    }

    public void StartGame()
    {
        _state = GameState.play;

        Score = 0;
        KillCount = 0;

        BallCount = 0;
    }
    public void EndGame()
    {
        _state = GameState.clear;
    }

}
