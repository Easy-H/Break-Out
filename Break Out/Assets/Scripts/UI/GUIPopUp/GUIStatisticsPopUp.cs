using UnityEngine;
using UnityEngine.UI;
using EHTool.UIKit;

public class GUIStatisticsPopUp : GUICustomPopUp
{
    [SerializeField] Text _ballCreateCount;
    [SerializeField] Text _ballOutCount;
    [SerializeField] Text _enemyKillCount;
    [SerializeField] Text _bestScore;

    public override void Open() {
        base.Open();
        _ballCreateCount.text = StatisticsManager.Instance.GetBallCreateCount().ToString();
        _ballOutCount.text = StatisticsManager.Instance.GetBallOutCount().ToString();
        _enemyKillCount.text = StatisticsManager.Instance.GetKillCount().ToString();
        _bestScore.text = StatisticsManager.Instance.GetBestScore().ToString();
    }
}
