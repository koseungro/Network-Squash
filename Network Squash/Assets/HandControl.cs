using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandControl : MonoBehaviour {
    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;

    private Animator anim;
    private int hashIsGrabbing = Animator.StringToHash ("IsGrabbing");
    private GameObject grabbedRacket;

    public Transform grabPos;

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
            anim.SetBool (hashIsGrabbing, false);
           
        }
    }

    void RacketGrab () 
    {       
        anim.SetBool (hashIsGrabbing, true);
    }

    void OnTriggerStay(Collider other)
    {
        if(other.CompareTag("RACKET"))
        {
             
            if(trigger.GetStateDown(hand))
            {
                Debug.Log("racket 잡자!");
                grabbedRacket = other.gameObject;
                grabbedRacket.transform.SetParent(transform);
                grabbedRacket.GetComponent<Rigidbody>().isKinematic = true;
                grabbedRacket.transform.localPosition = grabPos.localPosition;
                grabbedRacket.transform.localRotation = grabPos.localRotation;
                // grabbedRacket.transform.localPosition = new Vector3(0.06f, 0.0054f, 0.1f);
                // grabbedRacket.transform.localRotation = new Quaternion(254.0f, -7.23f, -136.0f, 0);
            }

            if(trigger.GetStateUp(hand))
            {
                // grabbedRacket.GetComponent<Rigidbody>().isKinematic = false;
                grabbedRacket.transform.parent = null;

            }
        }
        
    }
}