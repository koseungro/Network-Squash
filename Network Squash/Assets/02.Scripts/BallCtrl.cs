using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallCtrl : Photon.MonoBehaviour
{
    public static BallCtrl instance = null;
    
    private Rigidbody rb;
         
    public Vector3 ballPower;
    //private Vector3 initialVelocity;
    private Vector3 lastFrameVelocity;
    private float minVelocity = 5f;
    private AudioSource _audio;    

    public AudioClip bounceWall;
    
    Vector3 dir = new Vector3(1, 2, 2);



    void Start()
    {
        instance = this;

        rb = GetComponent<Rigidbody>();        
        _audio = GetComponent<AudioSource>();
        //rb.velocity = initialVelocity;

        ballPower = Vector3.forward;
    }


    void Update()
    {
        lastFrameVelocity = rb.velocity;
        //ballPower = Vector3.forward;

        if (Input.GetMouseButtonDown(0))
        {
            rb.AddForce(dir * 500f);
        }
    }


    public void OnCollisionEnter(Collision coll)
    {        
                
        if (coll.gameObject.CompareTag("WALL") || coll.gameObject.CompareTag("Player"))
        {
            Debug.Log("What!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!");
            _audio.PlayOneShot(bounceWall);
            
            Bounce(coll.contacts[0].normal);

            // Transform ball_Tr_atColl = transform;

            // photonView.RPC("Bounce", PhotonTargets.All, ball_Tr_atColl.position, ball_Tr_atColl.rotation, coll.contacts[0].normal);           
        }        

        //if (coll.gameObject.tag == "Goal1") 
        //{
        //    ScoreCtrl.instance.AddScore2();            
        //    Destroy(gameObject);
        //}

        if (coll.gameObject.tag == "Goal2" && photonView.isMine)
        {            
            ScoreCtrl.instance.AddScore1();                  
            
            Destroy(gameObject);
            
        }
        

    }

void Bounce(Vector3 collisionPoint)
{
    float speed = lastFrameVelocity.magnitude;
    Vector3 direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionPoint);

    rb.velocity = direction * Mathf.Max(speed, minVelocity) * 0.7f;
}

    // [PunRPC]
    // void Bounce(Vector3 ball_tr, Quaternion ball_Rot, Vector3 collisionPoint)
    // {
    //     //position 과 rotation 동기화
    //     transform.position = ball_tr;
    //     transform.rotation = ball_Rot;

    //     float speed = lastFrameVelocity.magnitude;
    //     Vector3 direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionPoint);

    //     rb.velocity = direction * Mathf.Max(speed, minVelocity) * 0.7f;
        
    // }
    // void Hit(Vector3 collisionNormal)
    // {
    //     float speed = lastFrameVelocity.magnitude;
    //     Vector3 direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionNormal);

    //     rb.velocity = direction * Mathf.Max(speed, 50);
    // }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("RACKET"))
        {
            Transform ball_Trans_Coll = transform;
            Vector3 racketVel = other.GetComponent<Racket>().racketVelocity;
            
            photonView.RPC("Hit", PhotonTargets.All, ball_Trans_Coll.position, ball_Trans_Coll.rotation, racketVel);
        }
    }


    [PunRPC]
    void Hit(Vector3 ballTr, Quaternion ballRot, Vector3 racket_Vel)
    {
        transform.position = ballTr;
        transform.rotation = ballRot;

        rb.velocity = racket_Vel + ballPower * 0.1f;
    }
    
}
