using UnityEngine;
using ObjectPool;

public class ObjectPoolCreator : MonoBehaviour
{
    [SerializeField] private ObjectPoolCreateData _createData;

    [SerializeField] private Transform[] _createPos = null;

    public void SetData(ObjectPoolCreateData data)
    {
        _createData = data;
    }

    public void Create()
    {

        for (int i = 0, createdCount = 0; i < _createPos.Length
            && createdCount < _createData.CreateCount; i++)
        {
            if (_createData.CreateCount - createdCount
                != _createPos.Length - i &&
                Random.Range(0, _createPos.Length)
                >= _createData.CreateCount) continue;

            GameObject created = CreateGameObject();
            created.transform.position = _createPos[i].position;
            createdCount++;

        }
    }

    protected virtual GameObject CreateGameObject()
    {
        if (_createData.CreateSound)
            _createData.CreateSound.Play();

        return ObjectPoolManager.Instance.GetGameObject(
            _createData.CreatePath[Random.Range(0, _createData.CreatePath.Length)]);
    }


}