using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Xml;
using System.Xml.Linq;
using UnityEngine;
using static UIManager;

public class PlayerManager : Singleton<PlayerManager>
{

    public class CharacterInfor {

        internal string Name;
        internal string Condition;
        internal string Sprite;
        internal int Id;

        internal void Read(XmlNode node)
        {
            Name = node.Attributes["name"].Value;
            Condition = node.Attributes["condition"].Value;
            Sprite = node.Attributes["sprite"].Value;
            Id = int.Parse(node.Attributes["id"].Value);
        }
    }

    List<CharacterInfor> _infor;
    public int NowKey { get; set; }

    string name;
    public string PlayerName {
        get { return name; }
        set {
            if (value.Equals(string.Empty)) {
                name = "Pong";
                return;
            }
            name = value;
        }
    }

    // Start is called before the first frame update
    protected override void OnCreate() {
        PlayerName = "Pong";
        _infor = new List<CharacterInfor>();

        XmlDocument xmlDoc = AssetOpener.ReadXML("CharacterInfor");

        XmlNodeList nodes = xmlDoc.SelectNodes("CharacterInfor/Character");

        for (int i = 0; i < nodes.Count; i++)
        {
            CharacterInfor infor = new CharacterInfor();
            infor.Read(nodes[i]);

            _infor.Add(infor);
        }
    }

    public List<CharacterInfor> GetCharacterInfor() {
        return _infor;
    }
}
