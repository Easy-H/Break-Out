using Newtonsoft.Json.Linq;
using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class SoundManager : MonoSingleton<SoundManager>
{
    class SoundData {
        public string name;
        public string path;

        public void Read(XmlNode node)
        {
            name = node.Attributes["name"].Value;
            path = node.Attributes["path"].Value;
        }
    }

    Dictionary<string, AudioClip> _dic;

    protected override void OnCreate()
    {
        base.OnCreate();

        _dic = new Dictionary<string, AudioClip>();
        XmlDocument xmlDoc = AssetOpener.ReadXML("SoundInfor");

        XmlNodeList nodes = xmlDoc.SelectNodes("List/Element");

        for (int i = 0; i < nodes.Count; i++)
        {
            SoundData soundData = new SoundData();
            soundData.Read(nodes[i]);

            _dic.Add(soundData.name, Resources.Load(soundData.path) as AudioClip);
        }

    }

    public void PlayEffect(string audioName)
    {
        AudioSource _audio = ObjectPoolManager.Instance.GetGameObject("SoundPlayer").GetComponent<AudioSource>();
        
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
