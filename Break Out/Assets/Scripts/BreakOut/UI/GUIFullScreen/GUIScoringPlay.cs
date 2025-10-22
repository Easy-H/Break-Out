public class GUIScoringPlay : GUIPlay {

    public override void GameOver()
    {
        base.GameOver();
        StatisticsManager.Instance.NewScore(Score);
        
    }

    public override void Retry()
    {
        base.Retry();
        OpenWindow("FullScreen_ScoringPlay");
        Close();
    }
}
