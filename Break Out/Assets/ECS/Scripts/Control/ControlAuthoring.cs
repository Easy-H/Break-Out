using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public struct ControlData : IComponentData {
    public float3 diff;

    public float2 min;
    public float2 max;
}


public class ControlAuthoring : MonoBehaviour
{
    public Vector3 _dir;
    public Vector2 _min;
    public Vector2 _max;

    public class Baker : Baker<ControlAuthoring>
    {
        public override void Bake(ControlAuthoring authoring)
        {

            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new ControlData
            {
                min = authoring._min,
                max = authoring._max,
                diff = authoring._dir
            });

        }

    }
    
}
