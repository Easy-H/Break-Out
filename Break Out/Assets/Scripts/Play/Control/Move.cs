using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] protected Vector3 _dir;

    [SerializeField] float minY;
    [SerializeField] UnityEngine.Events.UnityEvent _goalPointAction;

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < minY)
            _goalPointAction.Invoke();

        DoMove();
    }

    protected virtual void DoMove()
    {
        transform.Translate(_dir * Time.deltaTime);

    }

    public void DestroyScript()
    {
        Destroy(this);
    }

    public void DestroyGameObject() { 
        
    }
}
