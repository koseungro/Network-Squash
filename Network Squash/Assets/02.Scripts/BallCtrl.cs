using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCtrl : MonoBehaviour
{
    public static BallCtrl instance = null;
    
    private Rigidbody rb;
         
    public Vector3 ballPower;
    //private Vector3 initialVelocity;
    private Vector3 lastFrameVelocity;
    private float minVelocity = 5f;
    private AudioSource _audio;

    public AudioClip bounceWall;
    public AudioClip goal;




    Vector3 dir = new Vector3(1, 2, 2);


    void Start()
    {
        instance = this;

        rb = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
        //rb.velocity = initialVelocity;

        ballPower = Vector3.forward; //업데이트문으로?
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
                
        if (coll.gameObject.CompareTag("WALL") || coll.gameObject.CompareTag("Player"))
        {
            _audio.PlayOneShot(bounceWall);
            Bounce(coll.contacts[0].normal);
        }
        // if (coll.gameObject.CompareTag("RACKET"))
        // {

        //     Hit(coll.contacts[0].normal);
        // }

        if (coll.gameObject.tag == "Goal1") 
        {
            ScoreCtrl.instance.AddScore2();
            _audio.PlayOneShot(goal);
            Destroy(gameObject);
        }

        if (coll.gameObject.tag == "Goal2")
        {            
            ScoreCtrl.instance.AddScore1();
            _audio.PlayOneShot(goal);
            Destroy(gameObject);
            
        }
        

    }

    void Bounce(Vector3 collisionPoint)
    {
        float speed = lastFrameVelocity.magnitude;
        Vector3 direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionPoint);

        rb.velocity = direction * Mathf.Max(speed, minVelocity) * 0.7f;
        
    }
    // void Hit(Vector3 collisionNormal)
    // {
    //     float speed = lastFrameVelocity.magnitude;
    //     Vector3 direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

    //     rb.velocity = direction * Mathf.Max(speed, 50);
    // }
    
}
