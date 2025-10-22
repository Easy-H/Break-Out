using System.Collections.Generic;
using EasyH.Unity;
using EasyH;

public class PlayerManager : Singleton<PlayerManager>
{

    private IList<CharacterInfor> _infor;
    public int NowKey { get; set; }

    private string name;
    
    public string PlayerName
    {
        get { return name; }
        set
        {
            if (value.Equals(string.Empty))
            {
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

        IListConnector<string> lc = new JsonListConnector<string>();

        foreach (string s in lc.ReadData("CharacterInfor")) {
            _infor.Add(ResourceManager.Instance.ResourceConnector.
                Import<CharacterInfor>(s));
        }
    }

    public IList<CharacterInfor> GetCharacterInfor() {
        return _infor;
    }
}
