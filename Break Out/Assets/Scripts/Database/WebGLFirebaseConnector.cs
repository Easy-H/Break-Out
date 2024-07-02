using EHTool;
using EHTool.DBKit;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;


public static class WebGLBridge {

    [DllImport("__Internal")]
    public static extern void OnInit(string firebaseConfigValue);
    [DllImport("__Internal")]
    public static extern void PostJSON(string path, string value, string objectName, string callback, string fallback);
    [DllImport("__Internal")]
    public static extern void AddNewScore(string userId, string score);
    [DllImport("__Internal")]
    public static extern void GetJSON(string path, string objectName, string callback, string fallback);

}

public class WebGLFirebaseConnector<T> : MonoBehaviour, IDatabaseConnector<T> where T : IDictionaryable<T> {

    public int MaxScores = 20;

    public void Connect(string databaseName)
    {
        WebGLBridge.OnInit(AssetOpener.ReadTextAsset("FirebaseConfig"));

    }

    public bool IsDatabaseExist()
    {
        return true;
    }

    public virtual void AddRecord(T record)
    {
        WebGLBridge.PostJSON("Leader", JsonUtility.ToJson(record.ToDictionary()),
            gameObject.name, "AddRecordCallback", "AddRecordFallback");
    }

    public void UpdateRecordAt(T record, int idx)
    {
        WebGLBridge.PostJSON("Leader", JsonUtility.ToJson(record.ToDictionary()),
            gameObject.name, "AddRecordCallback", "AddRecordFallback");
    }

    public void GetRecordAt(CallbackMethod<T> callback, CallbackMethod fallback, int idx)
    {
        WebGLBridge.GetJSON("Leader",
            gameObject.name, "GetRecordAtCallback", "GetRecordAtFallback");
    }

    public void GetRecordAtCallback()
    {

    }
    public void GetRecordAtFallback()
    {

    }

    public void GetAllRecord(CallbackMethod<IList<T>> callback)
    {
        WebGLBridge.GetJSON("Leader",
            gameObject.name, "GetRecordAtCallback", "GetRecordAtFallback");
    }
}