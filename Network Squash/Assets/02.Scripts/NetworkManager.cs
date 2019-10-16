using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hashtable = ExitGames.Client.Photon.Hashtable;

public class NetworkManager : Photon.MonoBehaviour
{
	public Transform spawnPoint1;
	public Transform spawnPoint2;



	void Start()
	{
		photonView.RPC("StartGame", PhotonTargets.All);
	}

	[PunRPC]
	void StartGame()
	{
		
		Debug.Log("Joined Room!");
		PhotonNetwork.Instantiate("SquashPlayer", spawnPoint1.position, spawnPoint1.rotation, 0);
	}
}
