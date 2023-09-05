using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GUIScoringPlay : GUIPlay {

    public override void Retry()
    {
        UIManager.OpenGUI<GUIPlay>("ScoringPlay");
        Close();
        base.Retry();
    }
}
