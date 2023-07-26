using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Creator{

    [SerializeField] GameObject[] _created;
    [SerializeField] int _goalCreateCount = 1;

    [SerializeField] Transform[] createPos = null;

    [SerializeField] AudioSource createSound;

    [SerializeField] float _createCycleTime;
    [SerializeField] float _spendTimeAfterLastCreate = 0;

    [SerializeField] Transform _createdParent;

    public void SetCreatedParent(Transform parent) {
        _createdParent = parent;
    }

    public void SpendTime(float timeDelta) {
        _spendTimeAfterLastCreate += timeDelta;

        if (_spendTimeAfterLastCreate < _createCycleTime) return;

        _spendTimeAfterLastCreate -= _createCycleTime;

        Create();
    }

    public void Create()
    {
        for (int i = 0, j = 0; i < createPos.Length && j < _goalCreateCount; i++)
        {

            if (_goalCreateCount - j == createPos.Length - i || Random.Range(0, createPos.Length) < _goalCreateCount)
            {
                GameObject created = Object.Instantiate(_created[Random.Range(0, _created.Length)], createPos[i].position, Quaternion.identity);
                created.transform.SetParent(_createdParent);
                j++;

            }

        }

        if (createSound)
            createSound.Play();

    }

}