using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class NetworkMgr : Photon.PunBehaviour
{
  int[] list = new int[2];
  public Transform[] playerSpawnPoint;
	public Transform[] ballSpawnPoints;
  public Transform[] racketSpawnPoints;
    public Transform[] goalSpawnPoints;
  int myIndexNum;
  GameObject networkPlayer;
  PhotonView photonView;


	// public Transform player1Pos;

    // Start is called before the first frame update
    void Start()
    {
      photonView = GetComponent<PhotonView>();

      if(PhotonNetwork.isMasterClient)
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
      photonView.RPC("UpdatePlayerList", PhotonTargets.All, list, index);
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
        PhotonNetwork.Instantiate("GoalLine", goalSpawnPoints[myIndexNum].position, goalSpawnPoints[myIndexNum].rotation, 0);
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
