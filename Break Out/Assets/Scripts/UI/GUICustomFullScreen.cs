using UnityEngine;
using UnityEngine.SceneManagement;

public class GUICustomFullScreen : GUIFullScreen {

    protected override void Open()
    {
        UIManager.Instance.EnrollmentGUI(this);

    }

    public override void Close() {
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
