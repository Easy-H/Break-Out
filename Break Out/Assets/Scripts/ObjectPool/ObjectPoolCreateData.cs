using System;
using UnityEngine;

[Serializable]
public class ObjectPoolCreateData
{
    public string[] CreatePath;
    public int CreateCount = 1;

    public AudioSource CreateSound;
}
