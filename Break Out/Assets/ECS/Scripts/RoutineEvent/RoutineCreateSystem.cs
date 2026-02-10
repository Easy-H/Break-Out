using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial struct RoutineCreateSystem : ISystem
{

    public void OnCreate(ref SystemState state)
    {

    }

    public void OnDestroy(ref SystemState state)
    {

    }

    public void OnUpdate(ref SystemState state)
    {

        foreach (var (pos, create, routine) in
            SystemAPI.Query<RefRO<LocalToWorld>,RefRO<CreateData>, RefRW<RoutineEventData>>())
        {
            routine.ValueRW.SpendTime += SystemAPI.Time.DeltaTime;

            if (routine.ValueRW.SpendTime < routine.ValueRW.TimeCycle)
            {
                continue;
            }
            routine.ValueRW.SpendTime -= routine.ValueRW.TimeCycle;

            Spawn(ref state, pos, create);
        }
    }

    private void Spawn(ref SystemState state, RefRO<LocalToWorld> tr, RefRO<CreateData> create)
    {

        Entity newEntity = state.EntityManager.Instantiate(create.ValueRO.Target);

        var pos = tr.ValueRO.Position + create.ValueRO.Diff;

        state.EntityManager.SetComponentData(newEntity,
            LocalTransform.FromPosition(pos));

    }
}