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
    public Vector3 racketVelocity;
    private Transform tr;
    private Rigidbody rb;
    public GameObject player;
    public AudioClip racketDown;
    public AudioClip ballHit;
    private AudioSource _audio;
    private GameObject hit;
    private GameObject _hit;

    public float speed = 2.0f;

    public Transform racketRespawn1;

    private float curTime = 0f;
    private float hitTime = 0.25f;
    private bool canHit = true;

    void Start()
    {
        //pose = GetComponent<SteamVR_Behaviour_Pose>();
        //hand = pose.inputSource;
        tr = GetComponent<Transform>();
        rb = GetComponent<Rigidbody>();
        _audio = GetComponent<AudioSource>();
        hit = Resources.Load<GameObject>("Hit");
    }


    void Update()
    {
        curTime += Time.deltaTime;

        if (curTime < hitTime)
        {
            canHit = false;
        }
        else
        {
            canHit = true;
        }
        // if (trigger.GetStateDown(hand))
        // {
        //     RacketSwing();
        // }        
        if (grab.GetStateDown(hand))
        {
            RacketPosition();
        }

        RacketSwing();

    }

    private void OnTriggerEnter(Collider other)
    {

        if (canHit && other.transform.CompareTag("BALL"))
        {                        
            _hit = Instantiate(hit, other.transform.position, other.transform.rotation);

            other.GetComponent<Rigidbody>().velocity = racketVelocity + BallCtrl.instance.ballPower * 0.5f;
            _audio.PlayOneShot(ballHit);
            PlayerCtrl.instance.FaceChange();
            Debug.Log("Hit");

            Destroy(_hit, 1f);

            curTime = 0f;
        }
        if(other.transform.CompareTag("WALL"))
        {
            _audio.PlayOneShot(racketDown);
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
