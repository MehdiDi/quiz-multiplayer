using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GPlayer : MonoBehaviour {

    private int _playerID;
    [SerializeField]
    private string _playerName;
    private int _score = 0;

    public int Score
    {
        get
        {
            return _score;
        }

        set
        {
            _score = value;
        }
    }

    public int PlayerID
    {
        get
        {
            return _playerID;
        }

        set
        {
            _playerID = value;
        }
    }

    public string PlayerName
    {
        get
        {
            return _playerName;
        }

        set
        {
            _playerName = value;
            gameObject.name = _playerName;
        }
    }

 


}
