using System.Collections;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Realtime;

public class PhotonLobby: MonoBehaviourPunCallbacks {

    public GameObject BeginButton;
    public GameObject CancelButton;
    public GameObject LoadingPanel;
    public InputField PlayerName;
    
	void Start () {
        
        PhotonNetwork.ConnectUsingSettings();

        PhotonNetwork.AutomaticallySyncScene = true;
        UIController.instance.SetLoading(true);
        UIController.instance.ShowMessage("Connecting..");

    }


    public override void OnConnected()
    {
        base.OnConnected();

        BeginButton.SetActive(true);
        PlayerName.gameObject.SetActive(true);
        UIController.instance.HideMessage();
        UIController.instance.SetLoading(false);
        string _playerName = PlayerPrefs.GetString("PlayerName");

        if (_playerName != string.Empty)
        {
            PlayerName.text = _playerName;
        }
    }
 

    public void OnBeginClick()
    {
        BeginButton.SetActive(false);
        CancelButton.SetActive(true);
        PlayerName.gameObject.SetActive(false);
        PlayerPrefs.SetString("PlayerName", PlayerName.text);

        UIController.instance.SetLoading(true);

        PhotonNetwork.JoinRandomRoom();

    }
    public void OnCancelClick()
    {
        BeginButton.SetActive(true);
        CancelButton.SetActive(false);
        PhotonNetwork.LeaveRoom();
        UIController.instance.Loading.SetActive(false);
        UIController.instance.HideMessage();
    }

    public void SetLoading(bool active, String msg)
    {
        LoadingPanel.SetActive(active);
    }
    
    public override void OnJoinRandomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    public override void OnCreateRoomFailed(short returnCode, string message)
    {
        CreateRoom();
    }

    private void CreateRoom()
    {
        RoomOptions options = new RoomOptions() { IsOpen = true, IsVisible = true, MaxPlayers = (byte)GameSettings.instance.MaxPlayers };
        PhotonNetwork.CreateRoom("Room " + UnityEngine.Random.Range(1, 1000) ,options);
        
    }

    

    
}
