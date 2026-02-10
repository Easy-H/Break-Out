using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Physics;
using Unity.Physics.Systems;
using UnityEngine;

[UpdateInGroup(typeof(PhysicsSystemGroup))]
[UpdateAfter(typeof(PhysicsSimulationGroup))]
public partial struct BouncableMoveSystem : ISystem
{
    public void OnCreate(ref SystemState state)
    {
        state.RequireForUpdate<SimulationSingleton>();
    }

    public void OnUpdate(ref SystemState state)
    {
        var entities = SystemAPI.QueryBuilder().
            WithAll<BouncableMoveData>().Build().
                ToEntityArray(Allocator.Persistent);

        var moveDataLookUp = SystemAPI.
            GetComponentLookup<MoveData>(false);
        var velocityLookup = SystemAPI.
            GetComponentLookup<PhysicsVelocity>(false);

        state.Dependency = new BouncableMoveCollisionEventJob
        {
            BouncableMoveEntities = entities,
            PhysicsVelocityLookup = velocityLookup,
            MoveDataLookUp = moveDataLookUp
        }.Schedule(SystemAPI.
            GetSingleton<SimulationSingleton>(),
                state.Dependency);
    }

}

public partial struct BouncableMoveCollisionEventJob : ICollisionEventsJob
{
    [ReadOnly] public NativeArray<Entity> BouncableMoveEntities;
    public ComponentLookup<MoveData> MoveDataLookUp;
    public ComponentLookup<PhysicsVelocity> PhysicsVelocityLookup;

    public void Execute(CollisionEvent collisionEvent)
    {

        if (BouncableMoveEntities.Contains(collisionEvent.EntityA))
        {
            MoveDataChange(collisionEvent.EntityA, collisionEvent);
        }
        if (BouncableMoveEntities.Contains(collisionEvent.EntityB))
        {
            MoveDataChange(collisionEvent.EntityB, collisionEvent);
        }

    }

    private void MoveDataChange(Entity bouncableEntity,
        CollisionEvent collisionEvent)
    {
        
        // Bouncable 엔티티가 MoveData를 가지고 있는지 확인합니다.
        if (MoveDataLookUp.HasComponent(bouncableEntity))
        {
            // MoveData를 가져와서 값을 변경합니다.
            // 예를 들어, 충돌 법선(Normal)을 사용하여 속도(Velocity)를 반사(bounce)시키는 로직

            // MoveData를 RefRW로 가져와 직접 값을 수정합니다.
            MoveData moveData = MoveDataLookUp[bouncableEntity];
            float3 newVelocity = ReflectVelocity(moveData.dir, collisionEvent.Normal);
            moveData.dir = newVelocity;

            // 수정된 MoveData를 ComponentLookup을 통해 다시 설정합니다.
            MoveDataLookUp[bouncableEntity] = moveData;

            // 또는 ComponentLookup.SetComponent(entity, new MoveData { ... }); 를 사용할 수도 있습니다.
            // 또는 ComponentLookup.GetRefRW(bouncableEntity, out var moveDataRef)를 사용하여
            // moveDataRef.ValueRW.Velocity = newVelocity; 로 직접 수정할 수도 있습니다.
        }

        // 엔티티가 PhysicsVelocity를 가지고 있는지 확인합니다.
        if (PhysicsVelocityLookup.HasComponent(bouncableEntity))
        {
            // PhysicsVelocity를 RefRW로 가져와 직접 값을 수정합니다.
            // 이렇게 하면 물리 엔진이 계산한 속도를 0으로 덮어씁니다.
            ref var velocity = ref PhysicsVelocityLookup.GetRefRW(bouncableEntity).ValueRW;

            // 선형 속도 (Linear Velocity)와 각속도 (Angular Velocity)를 모두 0으로 고정합니다.
            velocity.Linear = float3.zero;
            velocity.Angular = float3.zero;
        }
    }

    // 속도를 반사하는 간단한 함수 (예시)
    private float3 ReflectVelocity(float3 velocity, float3 normal)
    {
        // V_new = V - 2 * (V . N) * N
        return math.normalize(velocity - 2 * math.dot(velocity, normal) * normal);
    }
}