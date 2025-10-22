using System.Collections.Generic;
using UnityEngine;
using EasyH.Unity;
using EasyH;

namespace ObjectPool {

    public class ObjectPoolManager : MonoSingleton<ObjectPoolManager> {

        private Dictionary<string, Pool> _dic;
        private Transform _createdBudget;

        public void SetBudget(Transform tr) {
            _createdBudget = tr;
        }

        public void CancleBudget(Transform tr)
        {
            if (_createdBudget != tr) return;
            _createdBudget = null;
        }

        protected override void OnCreate()
        {
            _dic = new Dictionary<string, Pool>();
            
            IDictionaryConnector<string, string> connector
                = new XMLDictionaryConnector<string, string>();

            foreach (var v in connector.ReadData("ObjectPoolTarget"))
            {
                _dic.Add(v.Key,
                    new Pool(v.Value, gameObject.transform));
            }
        }
        
        public GameObject GetGameObject(string key)
        {
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
