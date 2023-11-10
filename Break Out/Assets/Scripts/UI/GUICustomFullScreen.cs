using ObjectPool;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUICustomFullScreen : GUIFullScreen {

    [SerializeField] Transform _inforLine;
    [SerializeField] Transform _poolBudget;
    protected override void Open()
    {
        transform.SetParent(GameObject.FindWithTag("Canvas").transform);
        _inforLine.SetParent(GameObject.FindWithTag("InforLine").transform);
        UIManager.Instance.EnrollmentGUI(this);
        ObjectPoolManager.Instance.SetBudget(_poolBudget);

    }

    public override void Close()
    {
        _inforLine.SetParent(transform);
        base.Close();
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
