using Unity.Entities;
using Unity.Physics;
using Unity.Physics.Systems;
using Unity.Collections;

[UpdateInGroup(typeof(PhysicsSystemGroup))]
[UpdateAfter(typeof(BouncableMoveSystem))]
public partial class DamageSystem : SystemBase
{

    private EndSimulationEntityCommandBufferSystem
        _endSimulationEcbSystem;

    protected override void OnCreate()
    {
        _endSimulationEcbSystem = World.GetOrCreateSystemManaged<EndSimulationEntityCommandBufferSystem>();
    }

    protected override void OnUpdate()
    {
        var balls = SystemAPI.QueryBuilder().
            WithAll<BallData>().Build().
                ToEntityArray(Allocator.Persistent);

        var characters = SystemAPI.QueryBuilder().
            WithAll<CharacterData>().Build().
                ToEntityArray(Allocator.Persistent);

        var characterDataLookUp = SystemAPI.
            GetComponentLookup<CharacterData>(false);

        EntityCommandBuffer ecb =
            SystemAPI.GetSingleton<EndFixedStepSimulationEntityCommandBufferSystem.Singleton>()
            .CreateCommandBuffer(World.Unmanaged);

        Dependency = new BallCollisionEventJob
        {
            Ecb = ecb,
            Balls = balls,
            Characters = characters,
            CharacterLookUp = characterDataLookUp,
            EntityManager = EntityManager

        }.Schedule(SystemAPI.
            GetSingleton<SimulationSingleton>(), Dependency);
    }
}


public partial struct BallCollisionEventJob : ICollisionEventsJob
{
    public EntityCommandBuffer Ecb;
    public EntityManager EntityManager;
    [ReadOnly] public NativeArray<Entity> Balls;
    [ReadOnly] public NativeArray<Entity> Characters;
    public ComponentLookup<CharacterData> CharacterLookUp;

    public void Execute(CollisionEvent collisionEvent)
    {

        Entity entityA = collisionEvent.EntityA;
        Entity entityB = collisionEvent.EntityB;

        bool isEntityA_Ball = Balls.Contains(entityA);
        bool isEntityB_Ball = Balls.Contains(entityB);

        // 두 엔티티 중 하나만 BallData를 가지고 있어야 처리합니다.
        // (즉, Ball 엔티티가 Ball이 아닌 다른 물체와 충돌)
        if (isEntityA_Ball == isEntityB_Ball)
        {
            // 둘 다 BallData를 가지고 있거나, 둘 다 가지고 있지 않은 경우
            return;
        }

        Entity entity = isEntityA_Ball ? entityB : entityA;
        //int workIdx = isEntityA_Ball ? collisionEvent.BodyIndexB : collisionEvent.BodyIndexA;

        // 엔티티가 CharacterData를 가지고 있는지 확인합니다.
        if (!CharacterLookUp.HasComponent(entity))
        {
            return;
        }

        // CharacterData를 RefRW로 가져와 직접 값을 수정합니다.
        CharacterData characterData = CharacterLookUp[entity];
        characterData.HP--;

        if (characterData.HP < 1)
        {
            Ecb.DestroyEntity(entity);
            return;
        }

        CharacterLookUp[entity] = characterData;

        var hpUI = EntityManager.GetComponentObject<HPBarController>(entity);
        
        if (hpUI == null)
        {
            UnityEngine.Debug.Log("NO HP UI");
            return;
        }

        hpUI.UpdateUI(characterData.HP, characterData.MaxHP);

    }

}