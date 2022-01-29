using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] int damage = 1;

    public int GetDamage()
    {
        if (0 < damage--)
            return 1;
        else
            return 0;

    }
}
