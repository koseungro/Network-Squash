using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Racket : MonoBehaviour
{

    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;

    private Vector3 curRacket_pos;
    private Vector3 preRacket_pos;
    private Vector3 racketVelocity;

    public float speed = 3.0f;

    void Start()
    {

    }


    void Update()
    {
        RacketSwing();

        // if (trigger.GetStateDown(hand))
        // {
        //     RacketSwing();
        // }
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.CompareTag("BALL"))
        {
            other.GetComponent<Rigidbody>().velocity = racketVelocity + BallCtrl.instance.ballPower * speed;
        }
    }
    void RacketSwing()
    {
        curRacket_pos = transform.position;
        racketVelocity = (curRacket_pos - preRacket_pos) / Time.deltaTime;

        preRacket_pos = curRacket_pos;
    }
}
