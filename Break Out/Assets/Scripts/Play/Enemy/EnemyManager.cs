using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager
{
    static int createCount = 0;
    static int destroyByGongCount = 0;
    static int destroyByLaserCount = 0;

    static int nowEnemyCount;

    public static int CreateCount
    {
        get
        {
            return createCount;
        }
        set
        {
            createCount = value;
        }
    }
    public static int DestroyByGongCount
    {
        get
        {
            return destroyByGongCount;
        }
        set
        {
            destroyByGongCount = value;
        }
    }
    public static int DestroyByLaserCount
    {
        get
        {
            return destroyByLaserCount;
        }
        set
        {
            destroyByLaserCount = value;
        }
    }

    static void SceneStart()
    {
        createCount = 0;
        destroyByGongCount = 0;
        destroyByLaserCount = 0;
    }

    public static int NowEnemyCount()
    {
        nowEnemyCount = createCount - destroyByGongCount - destroyByLaserCount;
        
        return nowEnemyCount;
    }



}
