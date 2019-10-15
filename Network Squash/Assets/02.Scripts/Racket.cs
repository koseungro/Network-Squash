using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Racket : MonoBehaviour
{

    private Vector3 curRacket;
    private Vector3 preRacket;
    private Vector3 racketVel;
    private Vector3 ballHit;

    void Start()
    {
        // ballHit = BallCtrl.instance.ballVel;
    }


    void Update()
    {
        RacketSwing();
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.CompareTag("BALL"))
        {           
            float power = racketVel.magnitude;
            Debug.Log((power));
            // Vector3 dir = other.Vector3
            other.GetComponent<Rigidbody>().velocity = racketVel + BallCtrl.instance.ballPower*5f;
        }
    }
    void RacketSwing()
    {
        curRacket = transform.position;
        racketVel = (curRacket - preRacket) / Time.deltaTime;
        // float power = racketVel.magnitude;

        preRacket = curRacket;
    }
}
