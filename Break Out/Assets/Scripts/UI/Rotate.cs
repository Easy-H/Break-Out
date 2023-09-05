using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotate : MonoBehaviour
{

    [SerializeField] float _speed = 30f;
    float _amount = 0;

    // Update is called once per frame
    void Update()
    {
        _amount += Time.deltaTime;
        transform.Rotate(Vector3.forward, _speed * Time.deltaTime);
    }
}
