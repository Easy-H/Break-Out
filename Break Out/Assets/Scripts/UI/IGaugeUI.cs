using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IGaugeUI{
    public void InstantiateSet(int max);
    public void SetGauge(float amount);
}
