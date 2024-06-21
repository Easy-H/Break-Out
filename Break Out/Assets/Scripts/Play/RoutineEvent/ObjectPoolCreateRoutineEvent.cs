using System.Collections;
using UnityEngine;

namespace ObjectPool {

    public class ObjectPoolCreateRoutineEvent : RoutineEvent {

        [SerializeField] PoolCreateData _data;

        [SerializeField] Transform[] _createPos = null;

        [SerializeField] float _serialTerm = 0f;
        [SerializeField] int _serialCount = 1;

        RoutineEventExecutor _executor;

        int _leftCreateCount;

        public override void ExecuteEvent(RoutineEventExecutor executor)
        {
            _executor = executor;
            _leftCreateCount = _serialCount;

            StartCoroutine(_CreateObject());
        }

        IEnumerator _CreateObject()
        {

            yield return null;

            while (_leftCreateCount > 0)
            {

                for (int i = 0, createdCount = 0; i < _createPos.Length && createdCount < _data._goalCreateCount; i++)
                {

                    if (_data._goalCreateCount - createdCount != _createPos.Length - i
                        && Random.Range(0, _createPos.Length) >= _data._goalCreateCount) continue;

                    GameObject created = GetCreated();
                    created.transform.position = _createPos[i].position;
                    createdCount++;

                }

                if (_data._createSound)
                    _data._createSound.Play();

                _leftCreateCount--;

                yield return new WaitForSeconds(_serialTerm);
            }

            _executor.StartRoutine();
        }

        public void SetData(PoolCreateData newData) {
            _data = newData;
        }
        
        protected virtual GameObject GetCreated() {
            return ObjectPoolManager.Instance.GetGameObject(_data._created[Random.Range(0, _data._created.Length)]);
        }
    }
}