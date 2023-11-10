using UnityEngine;

namespace ObjectPool {

    public class ObjectPoolCreator : MonoBehaviour {

        [SerializeField] string[] _created;
        [SerializeField] int _goalCreateCount = 1;

        [SerializeField] Transform[] _createPos = null;

        [SerializeField] AudioSource _createSound;

        public void Create()
        {
            for (int i = 0, createdCount = 0; i < _createPos.Length && createdCount < _goalCreateCount; i++)
            {

                if (_goalCreateCount - createdCount != _createPos.Length - i && Random.Range(0, _createPos.Length) >= _goalCreateCount) continue;

                GameObject created = ObjectPoolManager.Instance.GetGameObject(_created[Random.Range(0, _created.Length)]);
                created.transform.position = _createPos[i].position;
                createdCount++;

            }

            if (_createSound)
                _createSound.Play();

        }

    }
}