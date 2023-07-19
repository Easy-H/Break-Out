using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public delegate bool ClearCheck();

public enum ClearCondition { 
    score, killcount
}

public class GUIStagePlay : GUIPlay
{

    [SerializeField] private GameObject _stageClear;

    ClearCheck _clearCondition;
    int _clearValue;


    public void SetStage(ClearCondition condition, int clearValue) {
        _clearValue = clearValue;

        switch (condition) { 
            case ClearCondition.score:
                _clearCondition = ScoreCheck;
                break;
            case ClearCondition.killcount:
                _clearCondition = KillCheck;
                break;
        }

    }

    bool ScoreCheck() {
        if (GameManager.Instance.GetScore() > _clearValue)
            return true;
        return false;
    }

    bool KillCheck()
    {
        if (GameManager.Instance.GetScore() > _clearValue)
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
