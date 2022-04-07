using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceableGo : Go
{
    float speed;

    protected override void Start()
    {
        speed = direct.magnitude;
        base.Start();
    }

    private void FixedUpdate()
    {

        direct = rb.velocity.normalized * speed;
    }

    protected override void Update()
    {
        base.Update();
        
    }

}
