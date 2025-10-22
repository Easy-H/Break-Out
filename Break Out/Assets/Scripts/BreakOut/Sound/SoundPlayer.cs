using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    public void PlayEffect(string audioName)
    {
        SoundManager.Instance.PlayEffect(audioName);

    }
}
