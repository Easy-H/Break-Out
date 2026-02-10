using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

public partial class ControlSystem : SystemBase
{
    protected override void OnUpdate()
    {
        float3 mousePos = Camera.main.
            ScreenToWorldPoint(Input.mousePosition);

        mousePos.z = 0;

        foreach (var (transform, control) in
            SystemAPI.Query<RefRW<LocalTransform>, RefRO<ControlData>>())
        {
            float3 pos = mousePos + control.ValueRO.diff;

            pos.x = Clamp(pos.x,
                control.ValueRO.min.x, control.ValueRO.max.x);
            pos.y = Clamp(pos.y,
                control.ValueRO.min.y, control.ValueRO.max.y);

            transform.ValueRW.Position = pos;
        }

    }

    float Clamp(float value, float min, float max)
    {
        if (value < min) return min;
        if (value > max) return max;
        return value;
    }
}