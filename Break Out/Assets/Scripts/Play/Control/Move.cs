using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] Vector3 _dir;

    [SerializeField] float minY;
    [SerializeField] UnityEngine.Events.UnityEvent _goalPointAction;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(_dir * Time.deltaTime);
        if (transform.position.y < minY)
            _goalPointAction.Invoke();
    }

    public void DestroyScript()
    {
        Destroy(this);
    }

    public void DestroyGameObject() { 
        
    }
}
