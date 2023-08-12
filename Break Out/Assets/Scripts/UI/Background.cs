using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] Creator _creator;
    // Start is called before the first frame update
    void Start()
    {
        _creator.SetCreatedParent(transform);
    }

    void Update() {
        _creator.SpendTime(Time.deltaTime);
    }

}
