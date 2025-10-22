using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyH.Unity;
using EasyH;

public class SoundManager : MonoSingleton<SoundManager>
{

    private Dictionary<string, AudioClip> _dic;

    protected override void OnCreate()
    {
        base.OnCreate();

        _dic = new Dictionary<string, AudioClip>();

        IDictionaryConnector<string, string> connector
            = new XMLDictionaryConnector<string, string>();

        foreach (var v in connector.ReadData("SoundInfor"))
        {
            _dic.Add(v.Key, ResourceManager.Instance.
                ResourceConnector.Import<AudioClip>(v.Value));
        }

    }

    public void PlayEffect(string audioName)
    {
        AudioSource _audio =
            ObjectPoolManager.Instance.
            GetGameObject(null, "SoundPlayer").
                GetComponent<AudioSource>();
        
        if (_dic.TryGetValue(audioName, out AudioClip value))
        {
            _audio.clip = value;

        }
        else if (audioName.Equals("NoSound"))
        {
            return;
        }
        else
        {
            _audio.clip = _dic["Error"];
        }

        _audio.Play();
    }

}
