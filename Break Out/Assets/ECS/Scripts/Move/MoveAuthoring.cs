using UnityEngine;
using Unity.Entities;
using Unity.Mathematics;

public struct MoveData : IComponentData {
    public float3 dir;
    public float speed;
}


public class MoveAuthoring : MonoBehaviour
{
    public Vector3 _dir = Vector3.up;
    public float _speed = 1f;

    public class Baker : Baker<MoveAuthoring>
    {
        public override void Bake(MoveAuthoring authoring)
        {

            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new MoveData
            {
                dir = authoring._dir.normalized,
                speed = authoring._speed
            });

        }

    }
    
}
