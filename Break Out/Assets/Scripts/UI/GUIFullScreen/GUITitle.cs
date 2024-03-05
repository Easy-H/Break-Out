using ObjectPool;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class GUITitle : GUICustomFullScreen
{
    [SerializeField] TextMeshProUGUI _scoreView;

    protected override void Open()
    {
        base.Open();
        UIManager.OpenGUI<GUIPopUp>("PopUp_Title");
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Time.timeScale = 0;
        }
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        SetBudget();
        _scoreView.text = string.Format("<mspace=\"0.45\">{0}</mspace>", StatisticsManager.Instance.GetBestScore().ToString("000000"));
    }
}
