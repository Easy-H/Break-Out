using System.Collections;
using System.Collections.Generic;
using TMPro.Examples;
using UnityEngine;

public abstract class Effect : MonoBehaviour
{
    [SerializeField] protected float _effectTime;

    public abstract void On(Vector3 pos);

}
