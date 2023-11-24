using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StatisticsManager : Singleton<StatisticsManager> {

    Dictionary<string, int> _killData;

    protected override void OnCreate()
    {
        base.OnCreate();
        _killData = new Dictionary<string, int>();
    }

    public void AddKillData(string killedEnemyName) {
        if (!GameManager.Instance.IsPlaying()) return;
        if (!_killData.ContainsKey(killedEnemyName)) {
            _killData.Add(killedEnemyName, 1);
            return;
        }

        _killData[killedEnemyName] = _killData[killedEnemyName] + 1;
    }

    public int GetKillCount() {
        int retval = 0;
        foreach (int count in _killData.Values) {
            retval += count;
        }
        return retval;
    }

    public int GetKillCount(string enemyName) {
        if (!_killData.ContainsKey(enemyName)) return 0;

        return _killData[enemyName];
    }

}
