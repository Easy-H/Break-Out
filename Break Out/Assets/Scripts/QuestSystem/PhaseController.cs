using JetBrains.Annotations;
using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseController : MonoBehaviour, IQuestReward
{
    [SerializeField] ObjectPoolCreator _phaseCreator;
    [SerializeField] PhaseData[] _data;
    int nowPhase = 0;

    public void GameStart()
    {
        PhaseChangeTo(0);
    }

    public void PhaseChangeTo(int idx) {

        if (idx >= _data.Length) return;

        nowPhase = idx;
        Time.timeScale = _data[idx].TimeScale;
        _phaseCreator.SetData(_data[idx].CreatorData);

        if (nowPhase + 1 >= _data.Length) return;

        QuestManager.Instance.AddQuest(QuestData.GetQuestChecker(_data[idx].QuestData), this);
    }

    public void Reward()
    {
        PhaseChangeTo(nowPhase + 1);
    }
}
