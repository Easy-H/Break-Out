﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Go : MonoBehaviour
{
    protected Rigidbody2D rb;

    [SerializeField] protected bool level = true;
    [SerializeField] bool enemy = false;
    [SerializeField] protected Vector3 direct = Vector3.down;

    protected virtual void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        rb.velocity = direct * PhaseManager.level;
    }

    protected virtual void Update()
    {
        if (level) {
            if (enemy && GameManager.coll != 0)
            {
                rb.velocity = Vector3.zero;
                return;
            }
            Debug.Log(PhaseManager.level);
            rb.velocity = direct * PhaseManager.level;
        }
        else {
            rb.velocity = direct;
        }
    }
    
}
