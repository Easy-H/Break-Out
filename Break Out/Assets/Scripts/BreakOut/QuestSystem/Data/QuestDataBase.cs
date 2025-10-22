using UnityEngine;

public abstract class QuestDataBase : ScriptableObject
{
    public abstract IQuestChecker GetQuestChecker();
    
}