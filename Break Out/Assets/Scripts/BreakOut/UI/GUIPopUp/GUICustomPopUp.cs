using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EasyH.Unity.UI;

public class GUICustomPopUp : GUIPopUp
{
    public override void Open()
    {
        base.Open();
        transform.position = Vector3.zero;
    }
}
