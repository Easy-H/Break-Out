using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIFullScreen : GUIWindow
{

    Stack<GUIPopUp> _popUpStack;
    GUIPopUp _nowPopUp;

    protected override void Open()
    {
        base.Open();
        UIManager.Instance.EnrollmentGUI(this);
        _popUpStack = new Stack<GUIPopUp>();
    }

    public void AddPopUp(GUIPopUp newPopUp) {
        if (_nowPopUp != null) {
            _popUpStack.Push(_nowPopUp);
            _nowPopUp.gameObject.SetActive(false);
        }
        _nowPopUp = newPopUp;
        _nowPopUp.transform.SetParent(transform);
    }

    public void NowPopUpClose() {
        _nowPopUp = _popUpStack.Pop();
        _nowPopUp.gameObject.SetActive(true);
    }
    public virtual void Stacked()
    {

    }

    public override void Close()
    {
        UIManager.Instance.Pop();
        base.Close();
    }

}
