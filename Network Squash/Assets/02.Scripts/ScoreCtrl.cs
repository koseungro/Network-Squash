using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCtrl : Photon.MonoBehaviour
{
    public static ScoreCtrl instance = null;
    public Transform[] ballSpawnPoints;

    //public GameObject ball;
    //public Transform ballRespawn1;
    //public Transform ballRespawn2;
    
    private int score1;
    private int score2;
    private Text text;

    void Start()
    {
        //시작할 때 상대방의 골대(Goal2) Photon.Instantiate하기

        instance = this;
        // Instantiate(ball, ballRespawn1.position, ballRespawn1.rotation);

        text = GetComponent<Text>();        
        score1 = 0;
        //score2 = 0;
    }


    void Update()
    {
        if (score1 < 0)
        {
            score1 = 0;
            //score2 = 0;
        }
        if(score1 >= 10)
        {
            Player1Win();
            
        }
        if (score2 >= 10)
        {
            Player2Win();
        }

        text.text = score1 + "  :  " + score2;
        //text.text = score1.ToString("00");
    }
    
    public void AddScore1()
    {
        if(score1 < 11)
        {
            score1++;            
            //StartCoroutine(RespawnBall1());
        }
    }
    public void AddScore2()
    {
        if (score2 < 11)
        {
            score2++;
            //StartCoroutine(RespawnBall2());
        }
    }
    void Player1Win()
    {
        //text.text = "Player1 Win!";
        VictoryText.instance.Player1Win();
        StartCoroutine(ScoreReset());
    }
    void Player2Win()
    {
        //text.text = "Player2 Win!";
        VictoryText.instance.Player2Win();
        StartCoroutine(ScoreReset());
    }

    //IEnumerator RespawnBall1()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    //Instantiate(ball, ballRespawn1.position, ballRespawn1.rotation);
    //    PhotonNetwork.InstantiateSceneObject("Ball_Network", ballSpawnPoints[0].position, ballSpawnPoints[0].rotation, 0, null);

    //}
    //IEnumerator RespawnBall2()
    //{
    //    yield return new WaitForSeconds(0.5f);
    //    //Instantiate(ball, ballRespawn2.position, ballRespawn2.rotation);
    //    PhotonNetwork.InstantiateSceneObject("Ball_Network", ballSpawnPoints[1].position, ballSpawnPoints[1].rotation, 0, null);
    //}

    IEnumerator ScoreReset()
    {
        yield return new WaitForSeconds(2f);
        score1 = 0;
        score2 = 0;
    }
}
