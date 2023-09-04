using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    [SerializeField] Creator _creatorReference;
    [SerializeField] int _count = 4;
    [SerializeField] float _turm = .2f;

    [SerializeField] Creator[] _creators;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateHP();
        SetCreator();
    }

    void SetCreator()
    {
        if (_count == 0) return;

        _creators = new Creator[_count];
        _creators[0] = _creatorReference;
        _creators[0].SetCreatedParent(transform.parent);

        for (int i = 1; i < _count; i++)
        {
            _creators[i] = new Creator(_creatorReference);
            _creators[i].SetCreatedParent(transform.parent);
            _creators[i].SpendTime(i * -_turm);
        }

    }

    void Update()
    {
        for (int i = 0; i < _count; i++)
        {
            _creators[i].SpendTime(Time.deltaTime);
        }

    }

    protected override void DieAct()
    {
        GameManager.Instance.EnemyKill();
        GameManager.Instance.AddScore(100);
        base.DieAct();
        Destroy(gameObject);
    }
}
