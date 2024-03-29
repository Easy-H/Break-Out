﻿/*
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Extensions;
*/

public class GUIScoringPlay : GUIPlay {

    public override void GameOver()
    {

        base.GameOver();
        StatisticsManager.Instance.NewScore(GameManager.Instance.Score);
        
        //DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public override void Retry()
    {
        base.Retry();
        UIManager.OpenGUI<GUIPlay>("FullScreen_ScoringPlay");
        Close();
    }
}
