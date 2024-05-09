using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

class Scoresort : IComparer<object> {

    public int Compare(object a, object b)
    {
        Dictionary<string, object> x = a as Dictionary<string, object>;
        Dictionary<string, object> y = b as Dictionary<string, object>;
        return -int.Parse(x["score"].ToString()).CompareTo(int.Parse(y["score"].ToString()));
    }
}

public class GUILeaderBoard : GUIPopUp, IObserver
{

    [SerializeField] ScoreUnit[] _infors;
    [SerializeField] Image _loading;

    protected override void Open()
    {
        base.Open();
        DatabaseManager.Instance.AddObserver(this);
        Notified();
    }

    public void Notified() {

        List<object> leaders = DatabaseManager.Instance.DBReader.Leaders;

        if (leaders == null) return;

        _loading.gameObject.SetActive(false);
        leaders.Sort(new Scoresort());

        for (int i = 0; i < _infors.Length; i++) {
            if (i < leaders.Count)
            {
                _infors[i].gameObject.SetActive(true);
                Dictionary<string, object> data = leaders[i] as Dictionary<string, object>;

                _infors[i].SetValue(i + 1, data["userId"].ToString(), data["score"].ToString());
                continue;
            }
            _infors[i].gameObject.SetActive(false);
        }

    }

    public override void Close()
    {
        DatabaseManager.Instance.RemoveObserver(this);
        base.Close();
    }

}
