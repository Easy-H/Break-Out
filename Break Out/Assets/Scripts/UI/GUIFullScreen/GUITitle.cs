using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUITitle : GUICustomFullScreen
{
    protected override void Open()
    {
        base.Open();
        UIManager.OpenGUI<GUIPopUp>("PopUp_Title");
    }
}
