using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NetworkMgr : Photon.MonoBehaviour
{
	public Transform player1Pos;
	public Transform ballSpawnPoint;

  public Transform racketSpawnPoint1;

  public Transform racketSpawnPoint2;

    // Start is called before the first frame update
    void Start()
    {
		PhotonNetwork.Instantiate("NetworkPlayer", player1Pos.position, player1Pos.rotation, 0);
		PhotonNetwork.InstantiateSceneObject("Ball_Network", ballSpawnPoint.position, ballSpawnPoint.rotation, 0, null);
    PhotonNetwork.Instantiate("Racket_Network", racketSpawnPoint1.position, racketSpawnPoint1.rotation, 0, null);
    PhotonNetwork.Instantiate("Racket_Network", racketSpawnPoint2.position, racketSpawnPoint2.rotation, 0, null);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
