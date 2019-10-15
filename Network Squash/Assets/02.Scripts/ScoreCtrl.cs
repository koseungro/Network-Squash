using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreCtrl : MonoBehaviour
{
    public int score1;
    public int score2;
    private Text text;

    void Start()
    {
        text = GetComponent<Text>();
        score1 = 0;
        score2 = 0;
    }

    
    void Update()
    {
        text.text = "" + score1;
    }
}
