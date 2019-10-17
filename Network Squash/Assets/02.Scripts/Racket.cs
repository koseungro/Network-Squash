using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Racket : MonoBehaviour
{
    //private SteamVR_Behaviour_Pose pose;

    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;
    public SteamVR_Action_Boolean grab = SteamVR_Actions.default_GrabGrip;

    private Vector3 curRacket_pos;
    private Vector3 preRacket_pos;
    private Vector3 racketVelocity;
    private Transform tr;
    private Rigidbody rb;
    public GameObject player;

    public float speed = 2.0f;

    public Transform racketRespawn1;    

    void Start()
    {
        //pose = GetComponent<SteamVR_Behaviour_Pose>();
        //hand = pose.inputSource;
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
    }


    void Update()
    {
        RacketSwing();

        // if (trigger.GetStateDown(hand))
        // {
        //     RacketSwing();
        // }        
        if (grab.GetStateDown(hand))
        {
            RacketPosition();
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.transform.CompareTag("BALL"))
        {
            other.GetComponent<Rigidbody>().velocity = racketVelocity + BallCtrl.instance.ballPower * 0.1f;
            PlayerCtrl.instance.FaceChange();
        }
    }
    void RacketSwing()
    {
        curRacket_pos = transform.position;
        racketVelocity = (curRacket_pos - preRacket_pos) / Time.deltaTime;

        preRacket_pos = curRacket_pos;
    }

    void RacketPosition() //Grab 버튼 누를 시 라켓을 손의 위치로
    {
        Vector3 racketpos = player.transform.position - gameObject.transform.position;
        float posDiff = racketpos.magnitude;
        Debug.Log(posDiff);
        if (posDiff >= 2)
        {
            //멀어진 라켓을 내 앞의 위치로
            tr.localPosition = racketRespawn1.transform.localPosition;
            tr.localRotation = racketRespawn1.transform.localRotation;
            rb.isKinematic = true;
            //멀어진 라켓 대신 새로운 라켓 생성?
        }
    }
}
