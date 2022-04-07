using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Clear : MonoBehaviour
{
    [SerializeField] GameObject[] clearActiveObject = null;


    void OnEnable()
    {
        Player.instance.Clear();

        for (int i = 0; i < clearActiveObject.Length; i++) {
            clearActiveObject[i].SetActive(true);
        }

    }
}
