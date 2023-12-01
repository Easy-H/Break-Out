using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GUIStatisticsPopUp : GUIPopUp
{
    [SerializeField] Text _ballCreateCount;
    [SerializeField] Text _ballOutCount;
    [SerializeField] Text _enemyKillCount;
    [SerializeField] Text _bestScore;

    protected override void Open() {
        base.Open();
        _ballCreateCount.text = StatisticsManager.Instance.GetBallCreateCount().ToString();
        _ballOutCount.text = StatisticsManager.Instance.GetBallOutCount().ToString();
        _enemyKillCount.text = StatisticsManager.Instance.GetKillCount().ToString();
        _bestScore.text = StatisticsManager.Instance.GetBestScore().ToString();
    }
}
