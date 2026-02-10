using UnityEngine;
using UnityEngine.Events;
using Unity.Entities;
using Unity.Mathematics;

public struct CreateData : IComponentData
{

    public Entity Target;
    public float3 Diff;

}

public class CreateAuthoring : MonoBehaviour
{

    public GameObject _target;
    public Transform _diff;

    public class Baker : Baker<CreateAuthoring>
    {
        public override void Bake(CreateAuthoring authoring)
        {
            if (authoring._target == null) return;
            if (authoring._diff == null) return;

            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent(entity, new CreateData
            {
                Target = GetEntity(authoring._target, TransformUsageFlags.Dynamic),
                Diff = authoring.transform.position - authoring._diff.position
            });

        }

    }
}
