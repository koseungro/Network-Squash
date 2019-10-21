using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class Racket : Photon.MonoBehaviour
{
    public static Racket instance = null;
    //private SteamVR_Behaviour_Pose pose;

    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;

    public SteamVR_Action_Vibration haptic = SteamVR_Actions.default_Haptic;

    private Vector3 curRacket_pos;
    private Vector3 preRacket_pos;
    public Vector3 racketVelocity;


    public AudioClip racketDown;
    public AudioClip ballHit;
    private AudioSource _audio;
    private GameObject hit;
    private GameObject _hit;

    public float speed = 2.0f;

    private float curTime = 0f;
    private float hitTime = 0.9f;
    private bool canHit = true;

    void Start()
    {
        instance = this;

        //pose = GetComponent<SteamVR_Behaviour_Pose>();
        //hand = pose.inputSource;

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

        RacketSwing();

    }

    private void OnTriggerEnter(Collider other)
    {

        if (canHit && other.transform.CompareTag("BALL"))
        {
            if(photonView.isMine)
            {
                Debug.Log("표정표정1");
                haptic.Execute(0.2f, 0.4f, 10f, 5f, hand);
                Debug.Log("표정표정2");
                PlayerCtrl.instance.FaceChange();
            }            
            _hit = Instantiate(hit, other.transform.position, other.transform.rotation);            
            // other.GetComponent<Rigidbody>().velocity = racketVelocity + BallCtrl.instance.ballPower * 0.5f;

            _audio.PlayOneShot(ballHit);            
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

}
