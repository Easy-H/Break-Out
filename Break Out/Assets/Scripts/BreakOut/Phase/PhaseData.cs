using UnityEngine;

[CreateAssetMenu(fileName = "Phase_", menuName = "Scriptable Object/PhaseData")]
public class PhaseData : ScriptableObject {
    public string QuestData;
    public ObjectPoolCreateData CreateData;
    public float TimeScale;
    
}