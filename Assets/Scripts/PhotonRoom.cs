using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public enum GameState
{
    waiting,
    started,
    ended
}
public class PhotonRoom : MonoBehaviourPunCallbacks
{
    public static PhotonRoom instance;
    private Hashtable players = new Hashtable();

    private GameState gameState;

    private int numberOfPlayers;
    [SerializeField]
    private PhotonView _photonView;

    public Hashtable Players
    {
        get
        {
            return players;
        }

        set
        {
            players = value;
        }
    }


    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
        }
        else
        {
            if (instance != this)
                Destroy(gameObject);
        }

        gameState = GameState.waiting;
        DontDestroyOnLoad(this);

    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.AddCallbackTarget(this);
        SceneManager.sceneLoaded += OnSceneLoaded;

    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();

        numberOfPlayers = PhotonNetwork.PlayerList.Length;
        string name = PlayerPrefs.GetString("PlayerName");
        PhotonNetwork.NickName = name;

        CheckPlayer();

        _photonView.RPC("RPC_UpdateNumberPlayers", RpcTarget.All);


    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        base.OnPlayerEnteredRoom(newPlayer);

        numberOfPlayers = PhotonNetwork.PlayerList.Length;

        

        
        if(numberOfPlayers == GameSettings.instance.MaxPlayers)
        {

        }

        CheckPlayer();
    }



    private void CheckPlayer()
    {


        if (!PhotonNetwork.IsMasterClient)
            return;

        if (numberOfPlayers == GameSettings.instance.MaxPlayers)
        {
            StartGame();
        }
    }


    private void StartGame()
    {
        gameState = GameState.started;
        PhotonNetwork.CurrentRoom.IsOpen = false;
        SceneManager.LoadScene(GameSettings.instance.GameScene);

        _photonView.RPC("LoadPlayers", RpcTarget.All);


    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == GameSettings.instance.GameScene)
        {
            RPC_CreatePlayer();
        }
    }

    public void AddScore(string PlayerName, int Points)
    {
        int _score = 0;
        foreach (DictionaryEntry entry in players)
        {
            if (entry.Key as string == PlayerName)
            {
                Debug.Log(entry.Key);
                _score = (int)entry.Value + Points;
                players[entry.Key] = _score;


                break;
            }
        }

        GameUIController.instance.UpdateScore(PlayerName, _score);
    }

    [PunRPC]
    public void RPC_CreatePlayer()
    {
        GameObject obj = PhotonNetwork.Instantiate("prefabs/PlayerObject", Vector3.zero, Quaternion.identity);
    }

    [PunRPC]
    public void RPC_UpdateNumberPlayers()
    {
        if (SceneManager.GetActiveScene().buildIndex != GameSettings.instance.GameScene)
            UIController.instance.ShowMessage("Waiting for players\n" + numberOfPlayers + "/" + GameSettings.instance.MaxPlayers);
    }

    [PunRPC]
    public void LoadPlayers()
    {
        Player[] _players = PhotonNetwork.PlayerList;

        for (int i = 0; i < _players.Length; i++)
        {
            players.Add(_players[i].NickName, 0);
        }
       
    }
}