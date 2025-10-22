using ObjectPool;
using UnityEngine;

public class PoolTargetRemoveAct : RemoveAct, IPoolTarget {

    Pool _parent;

    public override void DieAct()
    {
        if (_parent == null)
        {
            Destroy(gameObject);
            return;
        }
        _parent.ReturnObject(gameObject);
    }

    public void SetParentPool(Pool parent)
    {
        _parent = parent;
    }

    public void Return()
    {
    }
}