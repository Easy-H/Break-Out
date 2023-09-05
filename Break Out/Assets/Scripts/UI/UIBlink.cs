using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIBlink : MonoBehaviour
{
    [SerializeField] MaskableGraphic _target;

    [SerializeField] float _minTime = 1f;
    [SerializeField] float _maxTime = 4f;
    [SerializeField] float _blinkTime = .2f;

    Color _color;
    float _waitingTime = 0f;
    bool _blinking = false;


    void Start()
    {
        if (_target == null)
            _target = gameObject.GetComponent<MaskableGraphic>();
        _color = _target.color;
        _waitingTime = Random.Range(_minTime, _maxTime);
    }

    // Update is called once per frame
    void Update()
    {
        if (_blinking) return;

        _waitingTime -= Time.deltaTime;

        if (_waitingTime < 0) {
            _target.color = Color.clear;
            Invoke("BlinkAct", _blinkTime);
        }
    }

    private void BlinkAct()
    {
        _waitingTime = Random.Range(_minTime, _maxTime);
        _target.color = _color;
        _blinking = false;
    }



}
