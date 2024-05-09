using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using System;
using System.Linq;
using UnityEngine.Purchasing;
using Newtonsoft.Json;

#if !UNITY_WEBGL || UNITY_EDITOR
using Firebase.Database;
using Firebase.Extensions;

#endif

public class DatabaseManager : MonoSingleton<DatabaseManager> {

    public int MaxScores = 20;
    public IDatabaseReader DBReader { get; private set; }



    protected override void OnCreate()
    {
        DBReader = new FirebaseReader();
        DBReader.OnCreate();
    }

    public void AddScoreToLeaders(string userId, int score)
    {
        DBReader.AddScoreToLeaders(userId, score);
    }

    public void AddObserver(IObserver ops)
    {
        DBReader.AddObserver(ops);
    }

    public void RemoveObserver(IObserver ops)
    {
        DBReader.RemoveObserver(ops);
    }
}