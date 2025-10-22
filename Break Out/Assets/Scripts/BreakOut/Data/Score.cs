using System.Collections.Generic;
using EasyH.Tool.DBKit;

public class Score : IDictionaryable<Score>
{

    public string userId;
    public int score;

    public Score()
    {
        userId = "";
        score = 0;
    }

    public Score(string userId, int score)
    {
        this.userId = userId;
        this.score = score;
    }

    public IDictionary<string, object> ToDictionary()
    {
        IDictionary<string, object> newScoreMap = new Dictionary<string, object>();

        newScoreMap["userId"] = userId;
        newScoreMap["score"] = score;

        return newScoreMap;
    }

    public bool SetValueFromDictionary(
        IDictionary<string, object> value)
    {
        if (!value.ContainsKey("userId") ||
            !value.ContainsKey("score"))
            return false;

        userId = value["userId"].ToString();
        score = int.Parse(value["score"].ToString());

        return true;
    }

    public override string ToString()
    {
        return string.Format("{0} : {1}", userId, score);
    }

}