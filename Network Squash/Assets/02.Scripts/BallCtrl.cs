using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCtrl : MonoBehaviour
{
    public static BallCtrl instance = null;

    private Transform ballTr;
    private Rigidbody rb;
    
    private Vector3 curpos;
    private Vector3 prepos;
    // public Vector3 ballVel;
    public Vector3 ballPower;

    private Vector3 initialVelocity;
    private Vector3 lastFrameVelocity;
    private float minVelocity = 10f;

    Vector3 dir = new Vector3(1, 2, 2);


    void Start()
    {
        instance = this;

        rb = GetComponent<Rigidbody>();
        rb.velocity = initialVelocity;

        ballPower = Vector3.forward;
    }


    void Update()
    {
        lastFrameVelocity = rb.velocity;

        if (Input.GetMouseButtonDown(0))
        {
            rb.AddForce(dir * 500f);
        }
    }

    public void OnCollisionEnter(Collision coll)
    {        
        // curpos = coll.transform.position;
        // ballVel= curpos - prepos;

        // prepos = curpos; 
        
        if (coll.gameObject.CompareTag("WALL"))
        {            
            Bounce(coll.contacts[0].normal);
        }
        // if (coll.gameObject.CompareTag("RACKET"))
        // {

        //     Hit(coll.contacts[0].normal);
        // }
    }

    void Bounce(Vector3 collisionNormal)
    {
        float speed = lastFrameVelocity.magnitude;
        Vector3 direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        rb.velocity = direction * Mathf.Max(speed, minVelocity);
        
    }
    void Hit(Vector3 collisionNormal)
    {
        float speed = lastFrameVelocity.magnitude;
        Vector3 direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

        rb.velocity = direction * Mathf.Max(speed, 50);
    }
}
