using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BounceableGo : Go
{

    float value;

    protected override void Start()
    {
        base.Start();
        value = Mathf.PI - 0.5f;
    }

    protected override void Update()
    {
        direct = rb.velocity.normalized * direct.magnitude;
        base.Update();

        if (Mathf.Abs(Mathf.Atan2(rb.velocity.y, rb.velocity.x)) < 0.5f || Mathf.Abs(Mathf.Atan2(rb.velocity.y, rb.velocity.x)) > value)
        {
            rb.velocity = new Vector3(Mathf.Cos(0.5f) * Mathf.Sign(rb.velocity.x), Mathf.Sin(0.5f) * Mathf.Sign(rb.velocity.y), 0).normalized * direct.magnitude;
        }
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // 반사벡터 구하기
        Vector3 normalVector = collision.contacts[0].normal;
        Vector3 reflectVector = Vector3.Reflect(direct, normalVector);

        direct = reflectVector;

        if (Mathf.Abs(Mathf.Atan2(direct.y, direct.x)) < 0.5f) {
            direct = new Vector3(Mathf.Cos(0.5f) * Mathf.Sign(direct.x), Mathf.Sin(0.5f) * Mathf.Sign(direct.y), 0) * direct.magnitude;
        }
    }
    */
}
