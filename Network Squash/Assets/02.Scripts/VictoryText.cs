using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VictoryText : MonoBehaviour
{
    public static VictoryText instance = null;

    private Text text;

    void Start()
    {
        instance = this;
        text = GetComponent<Text>();
    }

    public void Player1Win()
    {
        text.text = "Player Win!!";
        Invoke("ResetText", 2f);
    }
    //public void Player2Win()
    //{
    //    text.text = "Player2 Win!!";
    //    Invoke("ResetText", 2f);
    //}
    void ResetText()
    {
        text.text = "";
    }
}
