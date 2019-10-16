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

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Player1Win()
    {
        text.text = "Player1 Win!!";
        Invoke("ResetText", 3f);
    }
    public void Player2Win()
    {
        text.text = "Player2 Win!!";
        Invoke("ResetText", 3f);
    }
    void ResetText()
    {
        text.text = "";
    }
}
