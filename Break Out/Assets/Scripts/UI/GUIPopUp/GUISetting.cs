using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class GUISetting : GUIPopUp {

    [SerializeField] private AudioMixer _audioMixer;
    [SerializeField] private Slider _musicMasterSlider;
    //[SerializeField] private Slider _musicBGMSlider;
    //[SerializeField] private Slider _musicSFXSlider;

    //https://wlsdn629.tistory.com/entry/%EC%9C%A0%EB%8B%88%ED%8B%B0-Audio-Mixer-%EC%82%AC%EC%9A%A9%EB%B0%A9%EB%B2%95
    protected override void Open()
    {
        base.Open();
        _musicMasterSlider.onValueChanged.AddListener(SetMasterVolume);

         _audioMixer.GetFloat("Master", out float volume);
        _musicMasterSlider.value = Mathf.Pow(10, volume / 20);
        //_musicBGMSlider.onValueChanged.AddListener(SetMusicVolume);
        //_musicSFXSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    public void SetMasterVolume(float volume)
    {
        _audioMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
    }

    public void SetMusicVolume(float volume)
    {
        _audioMixer.SetFloat("BGM", Mathf.Log10(volume) * 20);
    }

    public void SetSFXVolume(float volume)
    {
        _audioMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
    }
}