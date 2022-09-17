using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GongManager : MonoBehaviour {
    public static GongManager Instance;

    public Skill _skill = null;

    [SerializeField] GameObject _gong = null;

    public int NowBallCount { get; set; }
    [SerializeField] int _minBallCount = 0;

    [SerializeField] int _playerDamage = 1;

    public void Create() {
        Instantiate(_gong, Player.instance.transform.position + Vector3.up, Quaternion.identity);
    }

    public void BallDestroyed() {
        NowBallCount--;
        if (NowBallCount <= _minBallCount) {
            Player.instance.GetDamage(_playerDamage);
        }
    }

    private void Awake() {
        Instance = this;
    }


}
