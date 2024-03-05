using System.Collections;
using System.Collections.Generic;
using System.Xml;
using UnityEngine;

public class UIManager : Singleton<UIManager> {

    List<GUIFullScreen> uiStack;

    public GUIFullScreen NowDisplay { get; set; }

    public void EnrollmentGUI(GUIFullScreen newData)
    {
        uiStack.Add(newData);

        if (NowDisplay == null)
        {
            NowDisplay = newData;
            return;

        }
        else
        {
            NowDisplay.Stacked();
            NowDisplay.gameObject.SetActive(false);
        }

        NowDisplay = uiStack[uiStack.Count - 1];
        NowDisplay.gameObject.SetActive(true);

    }

    public void RemoveScreen(GUIFullScreen screen)
    {
        
        if (uiStack.Count < 1)
            return;
        if (NowDisplay == screen)
        {
            NowDisplay = uiStack[uiStack.Count - 2];
            NowDisplay.gameObject.SetActive(true);

        }

        uiStack.Remove(screen);

    }

    internal class GUIData {
        internal string name;
        internal string path;

        internal void Read(XmlNode node)
        {
            name = node.Attributes["name"].Value;
            path = node.Attributes["path"].Value;
        }
    }

    Dictionary<string, GUIData> _dic;

    protected override void OnCreate()
    {
        NowDisplay = null;
        uiStack = new List<GUIFullScreen>();

        _dic = new Dictionary<string, GUIData>();
        XmlDocument xmlDoc = AssetOpener.ReadXML("GUIInfor");

        XmlNodeList nodes = xmlDoc.SelectNodes("GUIInfor/GUIData");

        for (int i = 0; i < nodes.Count; i++)
        {
            GUIData guiData = new GUIData();
            guiData.Read(nodes[i]);

            _dic.Add(guiData.name, guiData);
        }

    }
    public static T OpenGUI<T>(string guiName)
    {
        string path = Instance._dic[guiName].path;
        T result = AssetOpener.Import<GameObject>(path).GetComponent<T>();

        return result;
    }

    GUIMessageBox _msg;

    public void ShowMessage(string data) {
        if (_msg == null) {
            _msg = OpenGUI<GUIMessageBox>("PopUp_Message");
        }
        _msg.SetMessage(data);
    }
}