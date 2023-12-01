using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIPopUp : GUIWindow
{
    protected override void Open()
    {
        if (UIManager.Instance.NowDisplay == null)
        {
            base.Open();
            return;
        }

        UIManager.Instance.NowDisplay.AddPopUp(this);
    }

    protected override void TransformSet()
    {
        RectTransform rect = gameObject.GetComponent<RectTransform>();

        rect.SetParent(UIManager.Instance.NowDisplay.transform);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, 400);
        rect.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 850);
    }

    public override void Close()
    {
        UIManager.Instance.NowDisplay.NowPopUpClose();
        base.Close();
    }
}
