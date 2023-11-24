using UnityEngine;

namespace ObjectPool {

    public class ObjectPoolCreator : MonoBehaviour {

        [SerializeField] PoolCreateData _data;

        [SerializeField] Transform[] _createPos = null;

        public void SetData(PoolCreateData newData) {
            _data = newData;
        }

        public void Create()
        {
            for (int i = 0, createdCount = 0; i < _createPos.Length && createdCount < _data._goalCreateCount; i++)
            {

                if (_data._goalCreateCount - createdCount != _createPos.Length - i
                    && Random.Range(0, _createPos.Length) >= _data._goalCreateCount) continue;

                GameObject created = ObjectPoolManager.Instance.GetGameObject(_data._created[Random.Range(0, _data._created.Length)]);
                created.transform.position = _createPos[i].position;
                createdCount++;

            }

            if (_data._createSound)
                _data._createSound.Play();

        }

    }
}