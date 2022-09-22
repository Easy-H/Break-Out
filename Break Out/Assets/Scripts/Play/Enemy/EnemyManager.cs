using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager {

    static int nowEnemyCount;

    public static int CreateCount { get; set; } = 0;
    public static int DestroyByGongCount { get; set; } = 0;
    public static int DestroyByLaserCount { get; set; } = 0;

    static void SceneStart() {
        CreateCount = 0;
        DestroyByGongCount = 0;
        DestroyByLaserCount = 0;
    }

    public static int NowEnemyCount() {
        nowEnemyCount = CreateCount - DestroyByGongCount - DestroyByLaserCount;

        return nowEnemyCount;
    }



}
