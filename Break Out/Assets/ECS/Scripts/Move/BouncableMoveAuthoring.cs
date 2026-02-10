using UnityEngine;
using Unity.Entities;

public struct BouncableMoveData : IComponentData
{
    
}

[RequireComponent(typeof(MoveAuthoring))]
public class BouncableMoveAuthoring : MonoBehaviour
{
    public class Baker : Baker<BouncableMoveAuthoring>
    {
        public override void Bake(BouncableMoveAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new BouncableMoveData());

        }
    }
}