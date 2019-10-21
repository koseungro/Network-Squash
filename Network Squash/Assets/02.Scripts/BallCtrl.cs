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
    //public Transform[] ballSpawnPoints;

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
                
        if (coll.gameObject.CompareTag("WALL"))
        {            
            _audio.PlayOneShot(bounceWall);
            
            Bounce(coll.contacts[0].normal);

            // Transform ball_Tr_atColl = transform;

            // photonView.RPC("Bounce", PhotonTargets.All, ball_Tr_atColl.position, ball_Tr_atColl.rotation, coll.contacts[0].normal);           
        }


		if(coll.gameObject.CompareTag("Player"))
		{
			_audio.PlayOneShot(bounceWall);

			Bounce(coll.contacts[0].normal);

			photonView.RPC("BallBouncePlayer", PhotonTargets.All, transform.position, transform.rotation, rb.velocity);

		}

        if (coll.gameObject.tag == "Goal1"&& PhotonNetwork.player.ID == 1)

        {

			photonView.RPC("GoalPlayer1", PhotonTargets.All);

        }

        if (coll.gameObject.tag == "Goal2" && PhotonNetwork.player.ID == 2)
        {

			photonView.RPC("GoalPlayer2", PhotonTargets.All);
		}
        

    }

	[PunRPC]
	void GoalPlayer1()
	{
		Destroy(gameObject);

		if (NetworkMgr.instance.s1 != null)
		{
			ScoreCtrl.instance.AddScore2();
		}
		if (NetworkMgr.instance.s2 != null)
		{
			ScoreCtrl2.instance.AddScore2();
		}
		BallMgr.instance.Ball2();
	}

	[PunRPC]
	void GoalPlayer2()
	{
		Destroy(gameObject);

		if (NetworkMgr.instance.s1 != null)
		{
			ScoreCtrl.instance.AddScore1();
		}
		if (NetworkMgr.instance.s2 != null)
		{
			ScoreCtrl2.instance.AddScore1();
		}
		BallMgr.instance.Ball1();
	}

	void Bounce(Vector3 collisionPoint)
	{
    float speed = lastFrameVelocity.magnitude;
    Vector3 direction = Vector3.Reflect(lastFrameVelocity.normalized, collisionPoint);

    rb.velocity = direction * Mathf.Max(speed, minVelocity) * 0.9f;
	}


	//공이 player 몸에 맞았을 때 동기화
	[PunRPC]
	void BallBouncePlayer(Vector3 ballBouncePlayerPos, Quaternion ballBouncePlayerRot, Vector3 ballBouncePlayerVel)
	{
		transform.position = ballBouncePlayerPos;
		transform.rotation = ballBouncePlayerRot;
		rb.velocity = ballBouncePlayerVel;
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

	
	void OnTriggerStay(Collider other)
	{
		if (other.CompareTag("HAND"))
		{
			if (other.GetComponent<HandControl>().isGrabbingBall == true)
			{
				transform.SetParent(other.transform);
				photonView.RPC("BallGrab", PhotonTargets.All, transform.position, transform.rotation);
				//Transform ball_Trans_Grab = transform;
			}

			else if (other.GetComponent<HandControl>().isGrabbingBall == false)
			{
				transform.parent = null;
				photonView.RPC("BallRelease", PhotonTargets.All, transform.position, transform.rotation, rb.velocity);
			}

		}

	}

		//if(other.CompareTag("MIDSYNC"))
		//{
		//	Transform ball_Trans_Mid = transform;
		//	Vector3 midBallVelocity = rb.velocity;
		//	photonView.RPC("MidSyncBall", PhotonTargets.All, ball_Trans_Mid.position, ball_Trans_Mid.rotation, midBallVelocity);
		//}
	

	//공이 RACKET에 쳐졌을때 RPC
	[PunRPC]
    void Hit(Vector3 ballPos, Quaternion ballRot, Vector3 racket_Vel)
    {
        transform.position = ballPos;
        transform.rotation = ballRot;

        rb.velocity = racket_Vel + ballPower * 1.2f;

    }

	//공이 손에 잡혀있을때 위치값 RPC
	[PunRPC]
	void BallGrab(Vector3 grabBallPos, Quaternion grabBallRot)
	{
		rb.isKinematic = true;

		transform.position = grabBallPos;
		transform.rotation = grabBallRot;

	}

	//공이 손에서 던져졌을때 RPC
	[PunRPC]
	void BallRelease(Vector3 releaseBallPos, Quaternion releaseBallRot, Vector3 releaseBallVel)
	{
		transform.parent = null;
		rb.isKinematic = false;
		transform.position = releaseBallPos;
		transform.rotation = releaseBallRot;
		rb.velocity = releaseBallVel;
	}

	//공이 중간지점에서 위치와 속도 RPC
	//[PunRPC]
	//void MidSyncBall(Vector3 midBallPos, Quaternion midBallRot, Vector3 midBallVel)
	//{
	//	transform.position = midBallPos;
	//	transform.rotation = midBallRot;
	//	rb.velocity = midBallVel;
	//}

}
