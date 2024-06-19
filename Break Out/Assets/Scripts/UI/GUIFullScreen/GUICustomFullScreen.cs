using ObjectPool;
using UnityEngine;
using UnityEngine.SceneManagement;
using EHTool.UIKit;
using System.Collections.Generic;

public class GUICustomFullScreen : GUIWindow, IGUIFullScreen {

    [SerializeField] Transform _inforLine;
    [SerializeField] Transform _poolBudget;

    private IList<IGUIPopUp> _popupUI;
    protected IGUIPopUp _nowPopUp;
    protected IGUIPanel _nowPanel;

    bool _isSetting = false;

    virtual protected void Start()
    {
        if (_isSetting) return;

        Open();
    }

    public override void Open()
    {
        SetOn();

        transform.SetParent(GameObject.FindWithTag("Canvas").transform);

        _isSetting = true;

        _popupUI = new List<IGUIPopUp>();

        UIManager.Instance.EnrollmentGUI(this);
    }

    public override void SetOn()
    {
        base.SetOn();
        if (_nowPopUp != null)
            _nowPopUp.SetOn();
        if (_nowPanel != null)
            _nowPanel.SetOn();

        _inforLine.gameObject.SetActive(true);
        ObjectPoolManager.Instance.SetBudget(_poolBudget);
    }

    public override void SetOff()
    {
        base.SetOff();

        if (_nowPopUp != null)
            _nowPopUp.SetOff();
        if (_nowPanel != null)
            _nowPanel.SetOff();

        _inforLine.gameObject.SetActive(false);
        ObjectPoolManager.Instance.CancleBudget(_poolBudget);
    }

    public virtual void AddPopUp(IGUIPopUp popUp)
    {
        if (_nowPopUp != null)
        {
            _popupUI.Add(_nowPopUp);
            _nowPopUp.SetOff();
        }
        _nowPopUp = popUp;

    }

    public virtual void PopPopUp()
    {
        if (_popupUI.Count == 0)
        {
            _nowPopUp = null;
            return;
        }

        _nowPopUp = _popupUI[_popupUI.Count - 1];
        _nowPopUp.SetOn();
        _popupUI.RemoveAt(_popupUI.Count - 1);
    }

    public void AddPanel(IGUIPanel panel)
    {
        if (_nowPanel != null)
        {
            _nowPanel.Close();
        }
        _nowPanel = panel;
    }

    public void ClosePanel()
    {
        _nowPanel = null;
    }

    public override void Close()
    {
        UIManager.Instance.Pop();

        while (_popupUI.Count > 0)
        {
            _nowPopUp.Close();
        }

        Destroy(_inforLine.gameObject);
        base.Close();
    }

    public virtual void Stacked()
    {
        _inforLine.SetParent(transform);

    }

    public void OpenScene(int idx)
    {
        _inforLine.SetParent(transform);
        SceneManager.LoadScene(idx);
    }

    public override void OpenWindow(string key)
    {
        _inforLine.SetParent(transform);
        base.OpenWindow(key);
    }

    public void PlayAnim(GUIAnimatedOpen gui)
    {
        gui.Open();
    }

}
