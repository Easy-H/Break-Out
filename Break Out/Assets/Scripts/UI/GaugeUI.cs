using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GaugeUI : MonoBehaviour {
    abstract public void InstantiateSet(int max);
    abstract public void SetGauge(float amount);
}
