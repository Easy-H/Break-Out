using UnityEngine;

[CreateAssetMenu(fileName = "Pool_", menuName = "Scriptable Object/CharacterData")]
public class CharacterInfor : ScriptableObject
{
    public int Id;

    public string Name;
    public string Condition;
    public QuestDataBase Quest;
    public string Sprite;
    
}