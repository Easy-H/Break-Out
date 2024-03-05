using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using System.Xml.Linq;
using UnityEngine;

public class DevicePoolCreator : ObjectPoolCreator
{
    [SerializeField] Transform _parent;

    // Start is called before the first frame update
    protected override GameObject GetCreated()
    {
        GameObject retval = base.GetCreated();
        retval.transform.parent = _parent;
        return retval;
    }
}
