using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

class Pool {
    string _path;
    List<GameObject> _pool;
    int _min;

    public Pool()
    {
        _path = string.Empty;
        _pool = new List<GameObject>();
        _min = 5;

    }

    public GameObject GetObject() {
        if (_pool.Count < _min) {
            for (int i = _pool.Count; i < _min; i++) {
                _pool.Add(AssetOpener.Import<GameObject>(_path));
            }
        }
        return _pool[0];
    }

    public void ReturnObject(GameObject obj) {
        _pool.Add(obj);
    }

}

public class ObjectPool : Singleton<ObjectPool>
{
    Dictionary<string, Pool> _dic;

    public GameObject GetGameObject(string key)
    {
        if ( _dic.ContainsKey(key)) {
            return _dic[key].GetObject();
        }

        return _dic[key].GetObject();
    }
}
