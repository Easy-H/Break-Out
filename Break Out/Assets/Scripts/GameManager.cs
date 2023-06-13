using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public enum GateState { 
    pause, play
}

public delegate void ScoreAct(int score);

public class GameManager : Singleton<GameManager>
{
    GateState _state;

    int _score;
    ScoreAct _actor;
    Player _player;

    int _ballCount;

    public bool IsPlaying()
    {
        return _state == GateState.play;

    }

    public void AddScore(int amount) {
        _score += amount;
        _actor.Invoke(_score);
    }

    public int GetScore() {
        return _score;
    }

    public void SetScoreView(ScoreAct act) {
        _actor = act;
    }

    public void SetPlayer(Player player) {
        _player = player;
    }

    public void BallIn() {
        _ballCount++;
    }

    public void BallOut()
    {
        if (_player == null)
            return;

        if (--_ballCount == 0)
            _player.GetDamaged(1);

    }

    public void StartGame() {
        _score = 0;
        _state = GateState.play;
        _ballCount = 0;
    }

}
