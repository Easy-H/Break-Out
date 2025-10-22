using ObjectPool;
using UnityEngine;

public class BasicRemoveAct : RemoveAct {

    public override void DieAct()
    {
        Destroy(gameObject);
    }

}