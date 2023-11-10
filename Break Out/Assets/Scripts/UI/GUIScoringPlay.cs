using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Firebase;
using Firebase.Extensions;
using Firebase.Database;

public class GUIScoringPlay : GUIPlay {

    public override void GameOver()
    {

        base.GameOver();
        
        DatabaseReference reference = FirebaseDatabase.DefaultInstance.RootReference;
    }

    public override void Retry()
    {
        UIManager.OpenGUI<GUIPlay>("ScoringPlay");
        Close();
        base.Retry();
    }
}
