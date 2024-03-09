#if UNITY_WEBGL && !UNITY_EDITOR

using System.IO;
using System.Runtime.InteropServices;
using UnityEditor;

namespace FirebaseWebGL.Scripts.FirebaseBridge
{
    public static class FirebaseDatabase
    {
        [DllImport("__Internal")]
        public static extern void PostJSON(string path, string value, string objectName, string callback, string fallback);
        [DllImport("__Internal")]
        public static extern void AddNewScore(string userId, string score);
        [DllImport("__Internal")]
        public static extern void GetJSON(string path, string objectName, string callback, string fallback);
    }
}
#endif