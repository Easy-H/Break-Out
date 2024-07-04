public class WebGLFirebaseScoreConnector : WebGLFirebaseConnector<Score> {

    public override void AddRecord(Score record)
    {
        WebGLBridge.AddNewScore(_dbName, record.userId, record.score.ToString());
    }

}