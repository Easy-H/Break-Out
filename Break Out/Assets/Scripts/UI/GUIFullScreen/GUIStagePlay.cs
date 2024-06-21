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
        }

    }

    bool ScoreCheck() {
        if (Score >= _clearValue)
            return true;
        return false;
    }

    public override void AddScore(int amount)
    {
        base.AddScore(amount);

        if (!_clearCondition()) return;

        StageClear();
    }

    void StageClear() {
        _stageClear.SetActive(true);
    }
}
