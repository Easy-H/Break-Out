using System.Collections.Generic;
using UnityEngine;
using EasyH.Unity;

namespace ObjectPool
{
    public class Pool
    {
        private string _path;

        private Transform tr;
        private Queue<GameObject> _pool;
        private int _min;

        public Pool()
        {
            _path = string.Empty;
            _pool = new Queue<GameObject>();
            _min = 3;

        }

        public Pool(string path, Transform parent)
        {
            tr = parent;
            _path = path;
            _pool = new Queue<GameObject>();
            _min = 5;

        }

        public GameObject GetObject()
        {
            if (_pool.Count < _min)
            {
                GameObject obj = ResourceManager.Instance.
                    ResourceConnector.ImportGameObject(_path);

                obj.transform.SetParent(tr);
                obj.SetActive(false);
                obj.GetComponent<IPoolTarget>().SetParentPool(this);

                _pool.Enqueue(obj);

                ObjectCreate();
            }

            GameObject target = _pool.Dequeue();
            target.SetActive(true);

            return target;
        }

        void ObjectCreate()
        {
            if (_pool.Count >= _min) return;

            ResourceManager.Instance.ResourceConnector.
                ImportGameObjectAsync(_path,
                    (obj) =>
                    {
                        obj.transform.SetParent(tr);
                        obj.SetActive(false);
                        obj.GetComponent<IPoolTarget>().
                            SetParentPool(this);
                        _pool.Enqueue(obj);

                        ObjectCreate();

                    },
                    (msg) =>
                    {

                    });
        }

        public void ReturnObject(GameObject obj)
        {
            _pool.Enqueue(obj);
            obj.transform.SetParent(tr);
            obj.SetActive(false);
        }

    }
}