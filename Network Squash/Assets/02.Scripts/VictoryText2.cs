﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryText2 : MonoBehaviour
{
    public static VictoryText2 instance = null;

    private Text text;

    void Start()
    {
        instance = this;
        text = GetComponent<Text>();
    }

    public void Player1Win()
    {
        text.text = "Player1 Win!!";
        Invoke("ResetText", 2f);
    }
    public void Player2Win()
    {
        text.text = "Player2 Win!!";
        Invoke("ResetText", 2f);
    }
    void ResetText()
    {
        text.text = "";
    }
}
