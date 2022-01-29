using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public bool touched = false;

    private void OnDestroy()
    {
        if (touched)
            GameManager.coll--;
    }

}
