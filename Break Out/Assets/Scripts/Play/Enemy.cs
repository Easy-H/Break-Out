using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Character {

    // Start is called before the first frame update
    void Start()
    {
        ShowHP();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void DieAct()
    {
        GameManager.Instance.AddScore(1000);
        Destroy(gameObject);
    }
}
