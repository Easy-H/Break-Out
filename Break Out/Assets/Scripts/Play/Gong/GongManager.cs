using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GongManager: MonoBehaviour
{
    public static GongManager instance;

    [SerializeField] GameObject gong = null;

    public int nowBallCount { get; set; }
    [SerializeField] int minBallCount = 0;

    [SerializeField] int playerDamage = 1;

    public void Create() {
        Instantiate(gong, Player.instance.transform.position + Vector3.up, Quaternion.identity);
    }

    public void BallDestroyed() {
        nowBallCount--;
        if (nowBallCount <= minBallCount) {
            Player.instance.GetDamage(playerDamage);
        }
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
    }


}
