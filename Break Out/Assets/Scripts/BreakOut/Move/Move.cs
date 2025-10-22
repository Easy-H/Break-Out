using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Move : MonoBehaviour
{
    [SerializeField] protected Vector2 _defaultDir;
    [SerializeField] protected float _power;

    [SerializeField] private float minY;
    [SerializeField] private UnityEvent _goalPointAction;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY)
            _goalPointAction.Invoke();

        DoMove();
    }

    protected virtual void DoMove()
    {
        transform.Translate(_power * Time.deltaTime * _defaultDir);
        
    }
    
}
