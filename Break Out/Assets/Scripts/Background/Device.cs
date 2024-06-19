using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EHTool;

public class Device : MonoSingleton<Device>
{
    [SerializeField] Transform _inforView;
    [SerializeField] Transform _playView;

    public void InforSet(GameObject inforData) {
        inforData.transform.SetParent(_inforView);
    }

}
