using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GongManager : MonoBehaviour {

    public static GongManager Instance { get; private set; }
    
    [SerializeField] GameObject _gong = null;

    public int NowBallCount { get; set; }
    [SerializeField] int _minBallCount = 0;

    [SerializeField] int _playerDamage = 1;

    public void Create() {
        Instantiate(_gong, Player.Instance.transform.position + Vector3.up, Quaternion.identity);
    }

    public void BallDestroyed() {
        NowBallCount--;
        if (NowBallCount <= _minBallCount) {
            Player.Instance.GetDamage(_playerDamage);
        }
    }

    private void Awake() {
        Instance = this;
    }


}
