using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectExecutor : MonoBehaviour {
    public static void PlayEffect(string key, Vector3 position, Transform parent)
    {
        Effect effect = ObjectPoolManager.Instance.GetGameObject(parent, key).GetComponent<Effect>();
        effect.On(position);

    }

    public void PlayEffect(string effectName)
    {
        PlayEffect(effectName, transform.position, null);
    }
}
