using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR;

public class HandControl : Photon.MonoBehaviour,IPunObservable
{
    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Action_Boolean trigger = SteamVR_Actions.default_InteractUI;

    private int hashIsGrabbingRacket = Animator.StringToHash ("IsGrabbingRacket");
    private int hashIsGrabbingBall = Animator.StringToHash("IsGrabbingBall");
    private Animator anim;
    private GameObject grabbedRacket;
    private GameObject grabbedBall;

    public Transform grabPosRacket;
    public Transform grabPosBall;

    private Vector3 prePos_Racket;
    private Vector3 curPos_Racket;
    private Vector3 racketVelocity;

    private Vector3 prePos_Ball;
    private Vector3 curPos_Ball;
    private Vector3 ballVelocity;

    //공 아웃라인
    private Material outlineMt;
    private MeshRenderer outline_Obj;
    private Material[] matarray;

    //라켓 아웃라인
    private Material racketMt_default1;
    private Material racketMt_default2;
    private Material racketOutline1;
    private Material racketOutline2;
    private MeshRenderer outline_racket;
    private Material[] matarray_r;

	public bool isGrabbingBall = false; //BallCtrl Script에 RPC에 쓰임

 

    void Start () 
    {
        
        anim = GetComponent<Animator> ();
        outlineMt = Resources.Load<Material>("Outline");
        racketMt_default1 = Resources.Load<Material>("RacketDefault1");
        racketMt_default2 = Resources.Load<Material>("RacketDefault2");
        racketOutline1 = Resources.Load<Material>("RacketOutline1");
        racketOutline2 = Resources.Load<Material>("RacketOutline2");
    }

    // Update is called once per frame
    void Update ()
     {
         if(photonView.isMine){
        if (trigger.GetStateDown (hand)) 
        {
            RacketGrab ();
        } 
        else if (trigger.GetStateUp (hand)) 
        {           
            anim.SetBool (hashIsGrabbingRacket, false);
           
        }

        //잡고 있는 라켓 velocity 구하기
        if(grabbedRacket)
        curPos_Racket = grabbedRacket.transform.position;
        racketVelocity = (curPos_Racket-prePos_Racket)/Time.deltaTime;

        prePos_Racket = curPos_Racket;


        //잡고 있는 공 velocity 구하기
        if(grabbedBall)
        curPos_Ball = grabbedBall.transform.position;
        ballVelocity = (curPos_Ball-prePos_Ball)/Time.deltaTime;

        prePos_Ball = curPos_Ball;

         }


    }

    void RacketGrab () 
    {       
        anim.SetBool (hashIsGrabbingRacket, true);
    }

    void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("BALL"))
        {
            outline_Obj = other.GetComponent<MeshRenderer>();
            highlightBall();

        }
        if(other.CompareTag("RACKET"))
        {          
            outline_racket = other.GetComponent<MeshRenderer>();
            highlightRacket();
        }
        
    }

    void OnTriggerStay(Collider other)
    {      
		if(photonView.isMine)
		{ 
			if(other.CompareTag("RACKET"))
			{
				//잡은 라켓 grabbedRacket으로 선언
				grabbedRacket = other.gameObject;

				if (trigger.GetState(hand))  
                    {
                    offlightRacket();
					if(grabbedRacket.GetComponent<PhotonView>().ownerId == PhotonNetwork.player.ID)
						{
						grabbedRacket.transform.position = grabPosRacket.position;
						grabbedRacket.transform.rotation = grabPosRacket.rotation;
						}
                    }

                if(trigger.GetStateDown(hand))
                    {
					if (grabbedRacket.GetComponent<PhotonView>().ownerId == PhotonNetwork.player.ID)
						{
						Transform holdTr = grabbedRacket.transform;
						grabbedRacket.GetComponent<Rigidbody>().isKinematic = true;
						photonView.RPC("HoldRacket", PhotonTargets.All, holdTr.position, holdTr.rotation, PhotonNetwork.player.ID);						
						}
					}

				if(trigger.GetStateUp(hand))
					{
					anim.SetBool (hashIsGrabbingRacket, false);
					offlightRacket();
					Transform releaseTr = grabbedRacket.transform;
					photonView.RPC("ReleaseRacket", PhotonTargets.All, releaseTr.position, releaseTr.rotation, PhotonNetwork.player.ID, racketVelocity);
					//grabbedRacket.GetComponent<Rigidbody>().AddForce(racketVelocity * 100); //100은 임시로 정한 던지는 힘크기
					
                }
			}

		}

        if(other.CompareTag("BALL"))
        {
            if(photonView.isMine)
            {
                if(trigger.GetStateDown(hand))
                {
                anim.SetBool (hashIsGrabbingBall, true);




					grabbedBall = other.gameObject;
                grabbedBall.transform.position = grabPosBall.position;
				grabbedBall.GetComponent<Rigidbody>().isKinematic = true;
				grabbedBall.transform.SetParent(transform);
				isGrabbingBall = true; //BallCtrl Script에 전달용 (RPC에 쓰임)
				}

				if (trigger.GetState(hand))
                {
                offlightBall();
                }

                if(trigger.GetStateUp(hand))
                {
                anim.SetBool (hashIsGrabbingBall, false);
				grabbedBall.transform.parent = null;
				grabbedBall.GetComponent<Rigidbody>().isKinematic = false;
                grabbedBall.GetComponent<Rigidbody>().AddForce(ballVelocity * 50); //50은 임시로 정한 던지는 힘크기
				isGrabbingBall = false; //BallCtrl Script에 전달용 (RPC에 쓰임)

				}
            }
        }
        
    }

    void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("BALL"))
        {
            offlightBall();
        }
        if(other.CompareTag("RACKET"))
        {
            offlightRacket();
        }
    }

    void highlightBall()
    {
            matarray = outline_Obj.materials;
            matarray[1] = outlineMt;
            outline_Obj.materials = matarray;
    }

    void offlightBall()
    {
        matarray[1] = null;
        outline_Obj.materials = matarray;
    }
    void highlightRacket()
    {
        matarray_r = outline_racket.materials;
        matarray_r[0] = racketOutline1;
        matarray_r[1] = racketOutline2;
        outline_racket.materials = matarray_r;

    }
    void offlightRacket()
    {
        matarray_r[0] = racketMt_default1;
        matarray_r[1] = racketMt_default2;
        outline_racket.materials = matarray_r;

    }

	//라켓 잡을 때 동기화
	[PunRPC]
	void HoldRacket(Vector3 posAtHold, Quaternion rotAtHold, int playerID)
	{
		GameObject[] heldRacket = GameObject.FindGameObjectsWithTag("RACKET");

		for (int i = 0; i < heldRacket.Length; i++)
			if (heldRacket[i].GetComponent<PhotonView>().ownerId == playerID)
			{
				heldRacket[i].transform.position = posAtHold;
				heldRacket[i].transform.rotation = rotAtHold;
				heldRacket[i].GetComponent<Rigidbody>().isKinematic = true;				
				heldRacket[i].transform.SetParent(this.gameObject.transform);
			}
	}

	//라켓 놓을 때 동기화
	[PunRPC]
	void ReleaseRacket(Vector3 posAtRelease, Quaternion rotAtRelease, int playerID, Vector3 racketVel)
	{
		GameObject[] heldRacket = GameObject.FindGameObjectsWithTag("RACKET");

		for (int i = 0; i < 2; i++)
			if (heldRacket[i].GetComponent<PhotonView>().ownerId == playerID)
			{
				heldRacket[i].transform.position = posAtRelease;
				heldRacket[i].transform.rotation = rotAtRelease;
				heldRacket[i].transform.parent = null;
				heldRacket[i].GetComponent<Rigidbody>().isKinematic = false;
				heldRacket[i].GetComponent<Rigidbody>().AddForce(racketVel * 100); //100은 임시로 정한 던지는 힘크기
			}
	}

	public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
	{

	}
}