using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class MoveSystem : SystemBase
{
    protected override void OnUpdate()
    {
        //Debug.Log("MoveSystem");

        foreach (var (transform, speed) in
            SystemAPI.Query<RefRW<LocalTransform>, RefRO<MoveData>>())
        {
            transform.ValueRW = transform.ValueRO.Translate(
                speed.ValueRO.dir * speed.ValueRO.speed * SystemAPI.Time.DeltaTime);
        }
    }
}