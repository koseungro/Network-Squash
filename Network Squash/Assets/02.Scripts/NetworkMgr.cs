using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Valve.VR;

public class NetworkMgr : Photon.PunBehaviour
{
    public static NetworkMgr instance = null;

    public SteamVR_Input_Sources hand = SteamVR_Input_Sources.Any;
    public SteamVR_Action_Boolean grab = SteamVR_Actions.default_GrabGrip;

    int[] list = new int[2];
  public Transform[] playerSpawnPoint;
	public Transform[] ballSpawnPoints;
  public Transform[] racketSpawnPoints;
    public Transform[] goalSpawnPoints;
    public Transform[] scoreSpawnPoints;
    public Transform[] winSpawnPoints;
    public Transform[] racketBackPoints;
    private int myIndexNum;

    private GameObject scoreMgr1;
    private GameObject scoreMgr2;
    private GameObject win1;
    private GameObject win2;

    public GameObject s1;
    public GameObject s2;

    private int myID;

   private GameObject networkPlayer;
    private GameObject networkRacket;
  PhotonView photonView;


	// public Transform player1Pos;

    void Start()
    {
        myID = PhotonNetwork.player.ID;
        Debug.Log("My ID : " + myID);

        instance = this;
      photonView = GetComponent<PhotonView>();
        scoreMgr1 = Resources.Load<GameObject>("ScoreMgr1");
        scoreMgr2 = Resources.Load<GameObject>("ScoreMgr2");
        win1 = Resources.Load<GameObject>("WinCanvas1");
        win2 = Resources.Load<GameObject>("WinCanvas2");

        if (PhotonNetwork.isMasterClient)
      {
        AddPlayer(PhotonNetwork.player.ID);
        PlayerSpawn();
        
      }
		// PhotonNetwork.Instantiate("NetworkPlayer", player1Pos.position, player1Pos.rotation, 0);
    if(PhotonNetwork.isMasterClient)
		PhotonNetwork.InstantiateSceneObject("Ball_Network", ballSpawnPoints[0].position, ballSpawnPoints[0].rotation, 0, null);
    // PhotonNetwork.InstantiateSceneObject("Racket_Network", racketSpawnPoint1.position, racketSpawnPoint1.rotation, 0, null);
    // PhotonNetwork.Instantiate("Racket_Network", racketSpawnPoint2.position, racketSpawnPoint2.rotation, 0, null);
    }
    private void Update()
    {
        if (grab.GetStateDown(hand))
        {
            RacketPosition();
        }
    }

    void AddPlayer(int _id)
    {
      int index = Array.IndexOf(list, 0);
      list[index] = _id;
      photonView.RPC("UpdatePlayerList", PhotonTargets.Others, list, index);
    }

    void RemovePlayer(int _id)
    {
      int index = Array.IndexOf(list, _id);
      list[index] = 0;

    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
      if(!PhotonNetwork.isMasterClient) return;
      AddPlayer(newPlayer.ID);
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
      if(!PhotonNetwork.isMasterClient) return;
      RemovePlayer(otherPlayer.ID);

    }

    void PlayerSpawn()
    {
      networkPlayer = PhotonNetwork.Instantiate("NetworkPlayer", playerSpawnPoint[myIndexNum].position, playerSpawnPoint[myIndexNum].rotation, 0);
      networkRacket =  PhotonNetwork.Instantiate("Racket_Network", racketSpawnPoints[myIndexNum].position, racketSpawnPoints[myIndexNum].rotation, 0);
        //PhotonNetwork.Instantiate("GoalLine", goalSpawnPoints[myIndexNum].position, goalSpawnPoints[myIndexNum].rotation, 0);
        if(PhotonNetwork.player.ID == 1)
        {
            s1 = Instantiate(scoreMgr1, scoreSpawnPoints[myIndexNum].position, scoreSpawnPoints[myIndexNum].rotation);
            Instantiate(win1, winSpawnPoints[myIndexNum].position, winSpawnPoints[myIndexNum].rotation);
        }
        if(PhotonNetwork.player.ID == 2)
        {
            s2 = Instantiate(scoreMgr2, scoreSpawnPoints[myIndexNum].position, scoreSpawnPoints[myIndexNum].rotation);
            Instantiate(win2, winSpawnPoints[myIndexNum].position, winSpawnPoints[myIndexNum].rotation);
        }

        Debug.Log("myIndexNum : " + myIndexNum);
    }

    [PunRPC]
    void UpdatePlayerList(int[] _list, int _index)
    {
      list = _list;

      if(networkPlayer == null)
      {
        myIndexNum = _index;
        PlayerSpawn();
      }
    }

    void RacketPosition() //Grab 버튼 누를 시 라켓을 손의 위치로
    {
        Vector3 racketpos = networkPlayer.transform.position - networkRacket.transform.position;
        float posDiff = racketpos.magnitude;

        if (posDiff >= 0.5f)
        {
            //멀어진 라켓을 내 앞의 위치로

            networkRacket.transform.localPosition = racketBackPoints[myIndexNum].localPosition;
            networkRacket.transform.localRotation = racketBackPoints[myIndexNum].localRotation;
            //Racket.instance.racketTr.localPosition = racketBackPoints[myIndexNum].localPosition;
            //Racket.instance.racketTr.localRotation = racketBackPoints[myIndexNum].localRotation;
            //tr.localPosition = racketRespawn1.transform.localPosition;
            //tr.localRotation = racketRespawn1.transform.localRotation;

            //Racket.instance.racketTr.GetComponent<Rigidbody>().isKinematic = true;
            networkRacket.GetComponent<Rigidbody>().isKinematic = true;
        }
    }
}
