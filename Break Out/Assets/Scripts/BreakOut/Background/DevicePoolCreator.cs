using UnityEngine;

public class DevicePoolCreator : ObjectPoolCreator
{
    [SerializeField] private Transform _parent;
    
    protected override GameObject CreateGameObject()
    {
        GameObject retval = base.CreateGameObject();
        retval.transform.parent = _parent;
        return retval;
    }
}
