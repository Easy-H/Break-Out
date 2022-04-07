using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go : MonoBehaviour
{
    protected Rigidbody2D rb;
    
    [SerializeField] protected Vector3 direct = Vector3.down;

    protected virtual void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = direct * PhaseManager.level;
    }

    protected virtual void Update()
    {
        rb.velocity = direct * PhaseManager.level;
    }
    
}
