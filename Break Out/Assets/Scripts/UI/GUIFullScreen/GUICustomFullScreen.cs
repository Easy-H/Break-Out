using ObjectPool;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GUICustomFullScreen : GUIFullScreen {

    [SerializeField] Transform _inforLine;
    [SerializeField] Transform _poolBudget;
    protected override void Open()
    {
        base.Open();

    }

    protected override void TransformSet()
    {
        transform.SetParent(GameObject.FindWithTag("Canvas").transform);
    }

    protected virtual void OnEnable()
    {
        _inforLine.SetParent(GameObject.FindWithTag("InforLine").transform);
    }

    protected void SetBudget() {
        ObjectPoolManager.Instance.SetBudget(_poolBudget);
    }

    public override void Stacked()
    {
        _inforLine.SetParent(transform);

    }

    public override void Close()
    {
        Destroy(_inforLine.gameObject);
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
