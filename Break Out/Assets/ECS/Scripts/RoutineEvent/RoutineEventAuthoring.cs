using UnityEngine;
using UnityEngine.Events;
using Unity.Entities;

public struct RoutineEventData : IComponentData {

    public float TimeCycle;
    public float SpendTime;

}

public class RoutineEventAuthoring : MonoBehaviour
{

    public float TimeCycle;
    public float SpendTime;

    public class Baker : Baker<RoutineEventAuthoring>
    {
        public override void Bake(RoutineEventAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new RoutineEventData
            {
                TimeCycle = authoring.TimeCycle,
                SpendTime = authoring.SpendTime

            });

        }

    }
}
