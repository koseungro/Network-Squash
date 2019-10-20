using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NetworkMgr : Photon.PunBehaviour
{
    public static NetworkMgr instance = null;
    
  int[] list = new int[2];
  public Transform[] playerSpawnPoint;
	public Transform[] ballSpawnPoints;
  public Transform[] racketSpawnPoints;
    public Transform[] goalSpawnPoints;
    public Transform[] scoreSpawnPoints;
    private int myIndexNum;

    private GameObject scoreMgr1;
    private GameObject scoreMgr2;

    public GameObject s1;
    public GameObject s2;

    GameObject networkPlayer;
  PhotonView photonView;


	// public Transform player1Pos;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
      photonView = GetComponent<PhotonView>();
        scoreMgr1 = Resources.Load<GameObject>("ScoreMgr1");
        scoreMgr2 = Resources.Load<GameObject>("ScoreMgr2");

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
      PhotonNetwork.Instantiate("Racket_Network", racketSpawnPoints[myIndexNum].position, racketSpawnPoints[myIndexNum].rotation, 0);
        //PhotonNetwork.Instantiate("GoalLine", goalSpawnPoints[myIndexNum].position, goalSpawnPoints[myIndexNum].rotation, 0);
        if(PhotonNetwork.player.ID == 1)
        {
            s1 = Instantiate(scoreMgr1, scoreSpawnPoints[myIndexNum].position, scoreSpawnPoints[myIndexNum].rotation);
        }
        if(PhotonNetwork.player.ID == 2)
        {
            s2 = Instantiate(scoreMgr2, scoreSpawnPoints[myIndexNum].position, scoreSpawnPoints[myIndexNum].rotation);
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



    // // Update is called once per frame
    // void Update()
    // {
        
    // }
}
