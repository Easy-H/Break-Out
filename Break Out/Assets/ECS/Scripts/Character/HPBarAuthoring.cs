using Unity.Entities;
using UnityEngine;

public class HPBarAuthoring : MonoBehaviour
{
    public StatusUIBase HPBar;

    public class Baker : Baker<HPBarAuthoring>
    {
        public override void Bake(HPBarAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);
            
        }
    }
}