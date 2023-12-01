using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public interface IQuestReward {
    public void Reward();
}

public class QuestManager : MonoSingleton<QuestManager>
{

    class QuestInfor {
        internal IQuestChecker _checker;
        internal IQuestReward _reward;

        internal QuestInfor(IQuestChecker checker, IQuestReward reward) { 
            _checker = checker;
            _reward = reward;
        }

    }

    List<QuestInfor> _doingQuest;

    public void AddQuest(IQuestChecker questChecker, IQuestReward reward) {

        QuestInfor newQuest = new QuestInfor(questChecker, reward);

        _doingQuest.Add(newQuest);

    }

    private void _QuestAchieve(QuestInfor infor)
    {
        infor._reward.Reward();
        _doingQuest.Remove(infor);

    }

    protected override void OnCreate()
    {
        _doingQuest = new List<QuestInfor>();

    }

    // Update is called once per frame
    void Update()
    {
        if (_doingQuest.Count == 0) return;

        List<QuestInfor> achieveQuest = new List<QuestInfor>();

        foreach (QuestInfor quest in _doingQuest) {
            if (!quest._checker.CheckAchieve()) continue;

            achieveQuest.Add(quest);
        }

        foreach (QuestInfor quest in achieveQuest) {
            _QuestAchieve(quest);
        }
    }
}
