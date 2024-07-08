using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using EHTool.UIKit;
using EHTool;
using System;
using System.Linq;

class Scoresort : IComparer<object> {

    public int Compare(object a, object b)
    {
        Dictionary<string, object> x = a as Dictionary<string, object>;
        Dictionary<string, object> y = b as Dictionary<string, object>;
        return -int.Parse(x["score"].ToString()).CompareTo(int.Parse(y["score"].ToString()));
    }
}

public class GUILeaderBoard : GUICustomPopUp, IObserver<IList<Score>> {

    [SerializeField] ScoreUnit[] _infors;
    [SerializeField] Image _loading;

#nullable enable
    private IDisposable? _cancellation;

    public override void Open()
    {
        base.Open();
        _cancellation = DatabaseManager.Instance.Subscribe(this);
        DatabaseManager.Instance.GetLeader();
    }

    public override void Close()
    {
        _cancellation?.Dispose();
        base.Close();
    }

    public void OnCompleted()
    {

    }

    public void OnError(Exception error)
    {

    }

    public void OnNext(IList<Score> value)
    {
        _loading.gameObject.SetActive(false);

        if (value == null) return;

        IEnumerable<Score> sortedEnum = value.OrderBy(f => -f.score);
        IList<Score> sortedList = sortedEnum.ToList();

        for (int i = 0; i < _infors.Length; i++)
        {
            if (i < sortedList.Count)
            {
                _infors[i].gameObject.SetActive(true);

                _infors[i].SetValue(i + 1, sortedList[i].userId, sortedList[i].score.ToString());
                continue;
            }
            _infors[i].gameObject.SetActive(false);
        }

    }
}
