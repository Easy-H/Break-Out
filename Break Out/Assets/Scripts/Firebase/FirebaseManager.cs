using System.Runtime.InteropServices;
using EasyH;

static class FirebaseWebGLConnectBridge
{

    [DllImport("__Internal")]
    public static extern void FirebaseConnect(string firebaseConfigValue);
}

public class FirebaseManager : Singleton<FirebaseManager>
{
    private bool _isConnect = false;

    public void SetConfig()
    {
#if !UNITY_WEBGL || UNITY_EDITOR
        return;
#endif
        if (_isConnect) return;

        string json = FileManager.Instance.
            FileConnector.Read("FirebaseConfig");
            
        FirebaseWebGLConnectBridge.FirebaseConnect(json);

        _isConnect = true;

    }
}