using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using EasyH.Unity.UI;

public class GUITitle : GUICustomFullScreen
{
    [SerializeField] private TextMeshProUGUI _scoreView;
    [SerializeField] private GameObject _defaultView;
    
    public override void AddPopUp(IGUIPopUp popUp)
    {
        base.AddPopUp(popUp);
        _defaultView.SetActive(_nowPopUp == null);

    }

    public override void ClosePopUp(IGUIPopUp popUp)
    {
        base.ClosePopUp(popUp);
        _defaultView.SetActive(_nowPopUp == null);
    }

    public override void SetOn()
    {
        base.SetOn();
        _player.GameStart();
        
        _scoreView.text = string.Format(
            "<mspace=\"0.45\">{0}</mspace>",
            StatisticsManager.Instance.
                GetBestScore().ToString("000000"));
    }
}
