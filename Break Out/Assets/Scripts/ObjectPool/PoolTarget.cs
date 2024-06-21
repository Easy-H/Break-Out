using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ObjectPool {

    public interface IPoolTarget {
        public void SetParentPool(Pool parent);
        public void Return();
    }

    public class PoolTarget : MonoBehaviour, IPoolTarget {
        Pool _parent;

        public void SetParentPool(Pool parent)
        {
            _parent = parent;
        }

        public void Return()
        {
            if (_parent == null)
            {
                Destroy(gameObject);
                return;
            }
            _parent.ReturnObject(gameObject);
        }
    }
}