using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySetter : MonoBehaviour
{
    enum EnemyClass { 
        Triangle, Circle
    }

    [SerializeField] EnemyClass _class;


}
