using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour {

    public static GameSettings instance;
    public int MaxPlayers = 3;
    
    public int GameScene = 2;
    public int AnswerTime = 4;
    public int AnswerPoints = 5;
    public int QuestionTime = 25;
    

    public void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
}
