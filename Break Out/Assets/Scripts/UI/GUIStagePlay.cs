using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

[System.Serializable]
public delegate bool ClearCheck();

[System.Serializable]
public enum ClearCondition { 
    score, killcount, bossKillCount
}

public class GUIStagePlay : GUIPlay
{

    [SerializeField] private GameObject _stageClear;

    [SerializeField] ClearCondition _condition;
    [SerializeField] int _conditionValue;
    [SerializeField] ClearCheck _clearCondition;
    int _clearValue;

    protected override void Start()
    {
        base.Start();
        SetStage(_condition, _conditionValue);
    }

    public void SetStage(ClearCondition condition, int clearValue) {
        _clearValue = clearValue;

        switch (condition) { 
            case ClearCondition.score:
                _clearCondition = ScoreCheck;
                break;
            case ClearCondition.killcount:
                _clearCondition = KillCheck;
                break;
            case ClearCondition.bossKillCount:
                _clearCondition = BossKillCheck;
                break;
        }

    }

    bool BossKillCheck()
    {
        if (GameManager.Instance.BossKillCount >= _clearValue)
            return true;
        return false;

    }

    bool ScoreCheck() {
        if (GameManager.Instance.Score >= _clearValue)
            return true;
        return false;
    }

    bool KillCheck()
    {
        if (GameManager.Instance.KillCount >= _clearValue)
            return true;
        return false;
    }

    public override void SetScore(int score)
    {
        base.SetScore(score);

        if (!_clearCondition()) return;

        StageClear();
    }

    void StageClear() {
        _stageClear.SetActive(true);
    }
}
