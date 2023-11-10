using Firebase;
using Firebase.Database;
using Firebase.Extensions;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LeaderBoardEntry {
    public string uid;
    public int score = 0;

    public LeaderBoardEntry(string uid, int score)
    {
        this.uid = uid;
        this.score = score;
    }

    public Dictionary<string, object> ToDictionary()
    {
        Dictionary<string, object> result = new Dictionary<string, object>();
        result["uid"] = uid.ToString();
        result["score"] = score.ToString();

        return result;
    }
}

public class FirebaseDataBase : MonoBehaviour {
    public int MaxScores = 1000;

    DatabaseReference reference;
    void Start()
    {
        reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public void WriteNewScore(string userId) //랭킹 업데이트
    {
        string key = reference.Child("scores").Push().Key;
        LeaderBoardEntry entry = new LeaderBoardEntry(userId, GameManager.Instance.Score);
        Dictionary<string, object> entryValues = entry.ToDictionary();

        Dictionary<string, object> childUpdates = new Dictionary<string, object>();
        childUpdates["/scores/" + key] = "0";
        childUpdates["/user-scores/" + userId + "/" + key] = "0";

        reference.UpdateChildrenAsync(childUpdates);
    }

    public void AddScoreToLeaders(string userId) //안전하게 랭킹 업데이트
    {
        reference.RunTransaction(mutableData =>
        {
            List<object> leaders = mutableData.Value as List<object>;

            if (leaders == null)
            {
                leaders = new List<object>();
            }
            else if (mutableData.ChildrenCount >= MaxScores)
            {
                long minScore = long.MaxValue;
                object minVal = null;
                foreach (var child in leaders)
                {
                    if (!(child is Dictionary<string, object>)) continue;
                    long childScore = (long)
                                ((Dictionary<string, object>)child)["score"];
                    if (childScore < minScore)
                    {
                        minScore = childScore;
                        minVal = child;
                    }
                }
                if (minScore > GameManager.Instance.Score)
                {
                    // The new score is lower than the existing 5 scores, abort.
                    return TransactionResult.Abort();
                }

                // Remove the lowest score.
                leaders.Remove(minVal);
            }

            // Add the new high score.
            Dictionary<string, object> newScoreMap =
                             new Dictionary<string, object>();
            newScoreMap["userId"] = userId;
            newScoreMap["score"] = GameManager.Instance.Score;
            leaders.Add(newScoreMap);
            mutableData.Value = leaders;
            return TransactionResult.Success(mutableData);
        });
    }
}