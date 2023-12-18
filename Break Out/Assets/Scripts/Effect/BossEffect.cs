using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEffect : Effect
{
    [SerializeField] int _createCount;

    public override void On(Vector3 pos)
    {
        for (int i = 0; i < _createCount; i++)
        {
            //Ball.CreateBall(pos).GetComponent<Bounceable>().SetDir(Random.insideUnitCircle);
        }
        EndEffect();
    }
}
