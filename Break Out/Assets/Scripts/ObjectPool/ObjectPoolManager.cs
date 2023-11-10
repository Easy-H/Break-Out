using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Xml;
using TMPro;
using UnityEngine;

namespace ObjectPool {

    public class Pool {
        string _path;

        Transform tr;
        Stack<GameObject> _pool;
        int _min;

        public Pool()
        {
            _path = string.Empty;
            _pool = new Stack<GameObject>();
            _min = 5;

        }
        public Pool(string path, Transform parent)
        {
            tr = parent;
            _path = path;
            _pool = new Stack<GameObject>();
            _min = 1;

        }

        public GameObject GetObject()
        {
            if (_pool.Count < _min)
            {
                for (int i = _pool.Count; i < _min; i++)
                {
                    GameObject obj = AssetOpener.ImportGameObject(_path);
                    obj.transform.SetParent(tr);
                    obj.SetActive(false);
                    obj.GetComponent<PoolTarget>().SetParentPool(this);
                    _pool.Push(obj);
                }
            }

            GameObject target = _pool.Pop();
            target.SetActive(true);

            return target;
        }

        public void ReturnObject(GameObject obj)
        {
            _pool.Push(obj);
            obj.transform.SetParent(tr);
            obj.SetActive(false);
        }

    }

    public class ObjectPoolManager : MonoSingleton<ObjectPoolManager> {

        Dictionary<string, Pool> _dic;
        Transform _createdBudget;

        public void SetBudget(Transform tr) {
            _createdBudget = tr;
        }

        protected override void OnCreate()
        {
            XmlDocument xmlDoc = AssetOpener.ReadXML("ObjectPoolTarget");

            XmlNodeList nodes = xmlDoc.SelectNodes("List/Element");

            _dic = new Dictionary<string, Pool>();

            for (int i = 0; i < nodes.Count; i++)
            {
                PoolData poolData = new PoolData();
                poolData.Read(nodes[i]);

                _dic.Add(poolData.name, new Pool(poolData.path, gameObject.transform));
            }
        }

        class PoolData {
            internal string name;
            internal string path;

            internal void Read(XmlNode node)
            {
                name = node.Attributes["name"].Value;
                path = node.Attributes["path"].Value;
            }
        }

        public GameObject GetGameObject(string key) {
            return GetGameObject(_createdBudget, key);
        }

        public GameObject GetGameObject(Transform parent, string key)
        {
            if (!_dic.ContainsKey(key))
            {
                return null;
            }
            GameObject retval = _dic[key].GetObject();
            retval.transform.SetParent(parent);

            return retval;

        }
    }
}
