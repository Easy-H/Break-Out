using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkin : MonoBehaviour {

    [SerializeField] GameObject[] models;

    // Start is called before the first frame update
    private void OnEnable()
    {

        for (int i = 0; i < models.Length; i++)
        {
            models[i].SetActive(PlayerManager.Instance.NowKey == i);
        }
    }
}
