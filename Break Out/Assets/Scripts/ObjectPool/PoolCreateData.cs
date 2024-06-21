using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pool_", menuName = "Scriptable Object/PoolCreateData")]
public class PoolCreateData : ScriptableObject {

    public string[] _created;
    public int _goalCreateCount = 1;

    public AudioSource _createSound;
}
