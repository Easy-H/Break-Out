using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using EHTool.UIKit;

public class GUITitle : GUICustomFullScreen
{
    [SerializeField] TextMeshProUGUI _scoreView;
    [SerializeField] GameObject _defaultView;

    public override void Open()
    {
        base.Open();
    }

    public override void AddPopUp(IGUIPopUp popUp)
    {
        base.AddPopUp(popUp);
        _defaultView.SetActive(_nowPopUp == null);

    }
    public override void PopPopUp()
    {
        base.PopPopUp();
        _defaultView.SetActive(_nowPopUp == null);
    }

    public override void SetOn()
    {
        base.SetOn();
        _scoreView.text = string.Format("<mspace=\"0.45\">{0}</mspace>", StatisticsManager.Instance.GetBestScore().ToString("000000"));
    }
}
