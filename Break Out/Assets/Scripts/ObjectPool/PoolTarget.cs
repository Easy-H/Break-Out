using UnityEngine;

namespace ObjectPool {

    public class PoolTarget : MonoBehaviour, IPoolTarget {
        
        private Pool _parent;

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