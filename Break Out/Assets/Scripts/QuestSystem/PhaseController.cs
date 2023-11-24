using JetBrains.Annotations;
using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseController : MonoBehaviour, QuestReward
{
    [SerializeField] ObjectPoolCreator _phaseCreator;
    [SerializeField] PhaseData[] _data;
    int nowPhase;

    private void Start()
    {
        PhaseChangeTo(nowPhase);
    }

    public void PhaseChangeTo(int idx) {

        Debug.Log("Change To " + idx);
        if (idx >= _data.Length) return;

        nowPhase = idx;
        _phaseCreator.SetData(_data[idx].CreatorData);

        if (nowPhase + 1 >= _data.Length) return;

        QuestManager.Instance.AddQuest(_data[idx].Quest, this);
    }

    public void Reward()
    {
        PhaseChangeTo(nowPhase + 1);
    }
}
