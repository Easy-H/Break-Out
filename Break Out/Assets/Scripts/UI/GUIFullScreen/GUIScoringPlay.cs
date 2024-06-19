using EHTool.UIKit;

public class GUIScoringPlay : GUIPlay {

    public override void GameOver()
    {
        base.GameOver();
        StatisticsManager.Instance.NewScore(GameManager.Instance.Score);
        
    }

    public override void Retry()
    {
        base.Retry();
        UIManager.Instance.OpenGUI<GUIPlay>("FullScreen_ScoringPlay");
        Close();
    }
}
