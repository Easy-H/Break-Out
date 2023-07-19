using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCreator : MonoBehaviour {

    [SerializeField] Creator _creator;

    // Update is called once per frame
    void Update()
    {
        _creator.SpendTime(Time.deltaTime);
    }
}
