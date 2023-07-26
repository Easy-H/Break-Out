using UnityEngine;
using UnityEngine.SceneManagement;

public class GUICustomFullScreen : GUIFullScreen {

    [SerializeField] Transform _inforLine;
    protected override void Open()
    {
        _inforLine.SetParent(GameObject.FindWithTag("InforLine").transform);
        UIManager.Instance.EnrollmentGUI(this);

    }

    public override void Close() {
        _inforLine.SetParent(transform);
        base.Close();
    }

    public void OpenScene(int idx)
    {
        SceneManager.LoadScene(idx);
    }

    public override void OpenWindow(string key) {
        base.OpenWindow(key);
    }
    public void PlayAnim(GUIAnimatedOpen gui)
    {
        gui.Open();
    }

}