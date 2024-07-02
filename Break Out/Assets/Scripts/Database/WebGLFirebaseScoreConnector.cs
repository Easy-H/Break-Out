public class WebGLFirebaseScoreConnector : WebGLFirebaseConnector<Score> {

    public override void AddRecord(Score record)
    {
        WebGLBridge.AddNewScore(record.userId, record.score.ToString());
    }

}