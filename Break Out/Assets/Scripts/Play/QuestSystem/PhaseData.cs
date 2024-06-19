using UnityEngine;

[CreateAssetMenu(fileName = "Phase_", menuName = "Scriptable Object/PhaseData")]
internal class PhaseData : ScriptableObject {
    public string QuestData;
    public PoolCreateData CreatorData;
    public float TimeScale;

}
