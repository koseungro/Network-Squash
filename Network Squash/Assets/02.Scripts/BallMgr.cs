using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMgr : Photon.MonoBehaviour
{
    public static BallMgr instance = null;

    public Transform[] ballSpawnPoints;

    void Start()
    {
        instance = this;
    }

    public void Ball1()
    {
        StartCoroutine(RespawnBall1());
    }
    public void Ball2()
    {
        StartCoroutine(RespawnBall2());
    }

    IEnumerator RespawnBall1()
    {
        yield return new WaitForSeconds(0.5f);
        Debug.Log("HHHHHHHHHHHH");
        //Instantiate(ball, ballRespawn1.position, ballRespawn1.rotation);
        PhotonNetwork.InstantiateSceneObject("Ball_Network", ballSpawnPoints[0].position, ballSpawnPoints[0].rotation, 0, null);

    }
    IEnumerator RespawnBall2()
    {
        yield return new WaitForSeconds(0.5f);
        //Instantiate(ball, ballRespawn2.position, ballRespawn2.rotation);
        PhotonNetwork.InstantiateSceneObject("Ball_Network", ballSpawnPoints[1].position, ballSpawnPoints[1].rotation, 0, null);
    }
}
