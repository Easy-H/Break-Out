using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creator : MonoBehaviour {
    [SerializeField] GameObject _created;
    [SerializeField] float _cycle;
    [SerializeField] int _goalCreateCount = 1;

    [SerializeField] Transform[] createPos = null;


    [SerializeField] AudioSource createSound;
    
    float _spendTime = 0;

    void Start()
    {
        createSound = gameObject.GetComponent<AudioSource>();

    }

    void Update()
    {
        AddTime();

        if (_spendTime < _cycle)
            return;

        _spendTime -= _cycle;
        Create();

    }

    protected virtual void AddTime()
    {
        _spendTime += Time.deltaTime;
    }

    protected virtual void Create()
    {
        for (int i = 0, j = 0; i < createPos.Length && j < _goalCreateCount; i++)
        {

            if (_goalCreateCount - j == createPos.Length - i || Random.Range(0, createPos.Length) < _goalCreateCount)
            {
                Instantiate(_created, createPos[i].position, Quaternion.identity);
                j++;

            }

        }

        if (createSound)
            createSound.Play();

    }

}