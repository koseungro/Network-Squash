using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandControl : MonoBehaviour {
    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;

    private int hashIsGrabbingRacket = Animator.StringToHash ("IsGrabbingRacket");
    private int hashIsGrabbingBall = Animator.StringToHash("IsGrabbingBall");
    private Animator anim;
    private GameObject grabbedRacket;
    private GameObject grabbedBall;

    public Transform grabPosRacket;
    public Transform grabPosBall;

    private Vector3 prePos_Ball;
    private Vector3 curPos_Ball;
    private Vector3 ballVelocity;

    void Start () 
    {
        anim = GetComponent<Animator> ();
    }

    // Update is called once per frame
    void Update ()
     {
        if (trigger.GetStateDown (hand)) 
        {
            RacketGrab ();
        } 
        else if (trigger.GetStateUp (hand)) 
        {           
            anim.SetBool (hashIsGrabbingRacket, false);
           
        }

        if(grabbedBall)
        curPos_Ball = grabbedBall.transform.position;
        ballVelocity = (curPos_Ball-prePos_Ball)/Time.deltaTime;

        prePos_Ball = curPos_Ball;

        // Debug.Log("ballvelocity = " + ballVelocity);

    }

    void RacketGrab () 
    {       
        anim.SetBool (hashIsGrabbingRacket, true);
    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("RACKET"))
        {
             
            if(trigger.GetStateDown(hand))
            {
                
                grabbedRacket = other.gameObject;
                grabbedRacket.transform.SetParent(transform);
                grabbedRacket.GetComponent<Rigidbody>().isKinematic = true;
                grabbedRacket.transform.localPosition = grabPosRacket.localPosition;
                grabbedRacket.transform.localRotation = grabPosRacket.localRotation;

            }

            if(trigger.GetStateUp(hand))
            {
                // grabbedRacket.GetComponent<Rigidbody>().isKinematic = false;
                grabbedRacket.transform.parent = null;

            }
        }

        else if(other.CompareTag("BALL"))
        {
            if(trigger.GetStateDown(hand))
            {
                anim.SetBool (hashIsGrabbingBall, true);

                grabbedBall = other.gameObject;
                grabbedBall.transform.SetParent(transform);
                grabbedBall.GetComponent<Rigidbody>().isKinematic = true;
                grabbedBall.transform.localPosition = grabPosBall.localPosition;
                
            }

            if(trigger.GetStateUp(hand))
            {
                anim.SetBool (hashIsGrabbingBall, false);
                grabbedBall.transform.parent = null;
                grabbedBall.GetComponent<Rigidbody>().isKinematic = false;
                grabbedBall.GetComponent<Rigidbody>().AddForce(ballVelocity * 100); //100은 임시로 정한 던지는 힘크기
            }
        }
        
    }
}