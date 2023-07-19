using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    [SerializeField] Creator _creator;

    // Start is called before the first frame update
    void Start()
    {
        InstantiateHP();
        _creator.SetCreatedParent(transform.parent);
    }

    void Update()
    {
        _creator.SpendTime(Time.deltaTime);

    }

    protected override void DieAct()
    {
        GameManager.Instance.AddScore(1000);
        base.DieAct();
        Destroy(gameObject);
    }
}
