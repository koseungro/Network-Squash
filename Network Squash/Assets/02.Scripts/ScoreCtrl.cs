using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCtrl : MonoBehaviour
{
    public static ScoreCtrl instance = null;
                
    public GameObject ball;
    public Transform ballRespawn1;
    public Transform ballReapawn2;
        
    private int score1;
    private int score2;
    private Text text;

    void Start()
    {
        instance = this;
        Instantiate(ball, ballRespawn1.position, ballRespawn1.rotation);

        text = GetComponent<Text>();
        score1 = 0;
        score2 = 0;
    }


    void Update()
    {
        if (score1 < 0 || score2 < 0)
        {
            score1 = 0;
            score2 = 0;
        }
        if(score1 >= 10)
        {
            Player1Win();
        }
        if(score2 >= 10)
        {
            Player2Win();
        }

        text.text = score1 + "  :  " + score2;
    }
    
    public void AddScore1()
    {
        if(score1 < 11)
        {
            score1++;            
            StartCoroutine(RespawnBall1());
        }
    }
    public void AddScore2()
    {
        if(score2 < 11)
        {
            score2++;            
            StartCoroutine(RespawnBall2());
        }
    }
    void Player1Win()
    {
        text.text = "Player1 Win!";

        score1 = 0;
        score2 = 0;
    }
    void Player2Win()
    {
        text.text = "Player2 Win!";

        score1 = 0;
        score2 = 0;
    }
    IEnumerator RespawnBall1()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(ball, ballRespawn1.position, ballRespawn1.rotation);

    }
    IEnumerator RespawnBall2()
    {
        yield return new WaitForSeconds(0.5f);
        Instantiate(ball, ballRespawn1.position, ballRespawn1.rotation);
    }
}
