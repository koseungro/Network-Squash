using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PhotonConnectManager : Photon.PunBehaviour
{
    public int SceneIndexToMove = 1;
    public int MaxPlayerCountInRoom = 4;
    public bool useName;

    [HideInInspector]public GameObject NameUISet;
    [HideInInspector]public GameObject StatusTextObj;
    InputField inputField;

    // Start is called before the first frame update
    void Start()
    {
        if (useName){
            NameUISet.SetActive(true);
            inputField = GameObject.Find("PlayerName_InputField").GetComponent<InputField>();
            inputField.text = PlayerPrefs.GetString("PlayerName");
        } else {
            StartConnect();
            StatusTextObj.SetActive(true);
        }
    }

    public void StartConnect()
    {
        if (useName) PhotonNetwork.playerName =	inputField.text;
        
        NameUISet.SetActive(false);
        StatusTextObj.SetActive(true);

        PhotonNetwork.automaticallySyncScene = true;
        PhotonNetwork.ConnectUsingSettings("1");
    }

    public override void OnConnectedToMaster(){
        PhotonNetwork.JoinRandomRoom();
    }
    public override void OnPhotonRandomJoinFailed(object[] codeAndMsg){
        RoomOptions op = new RoomOptions();
        op.MaxPlayers = (byte)MaxPlayerCountInRoom;
        PhotonNetwork.CreateRoom(null, op, null);
    }
    public override void OnJoinedRoom(){
        PhotonNetwork.LoadLevel(SceneIndexToMove);
    }

    public void setPlayerName(string _name){
        PlayerPrefs.SetString("PlayerName", _name);
    }
}
