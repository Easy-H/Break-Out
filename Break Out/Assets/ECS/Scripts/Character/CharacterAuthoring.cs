using UnityEngine;
using Unity.Entities;

public struct CharacterData : IComponentData {
    public int HP;
    public int MaxHP;
}

public class CharacterAuthoring : MonoBehaviour
{
    public int HP;
    public int MaxHP;
    public HPBarController HPEntity;

    public class Baker : Baker<CharacterAuthoring>
    {
        public override void Bake(CharacterAuthoring authoring)
        {
            var entity = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<LinkedEntityGroup>(entity);

            AddComponent(entity, new CharacterData
            {
                HP = authoring.HP,
                MaxHP = authoring.MaxHP
            });

            if (authoring.HPEntity == null) return;

            AddComponentObject(entity, authoring.HPEntity);

        }

    }

}
