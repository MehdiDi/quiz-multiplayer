using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour {

    public static UIController instance;

    public Text MsgText;
    public GameObject Loading;
    

    private void Awake()
    {
        
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }
    public void ShowMessage(string message)
    {
        MsgText.gameObject.SetActive(true);
        MsgText.text = message;

    }

    public void SetLoading(bool loading)
    {
        Loading.SetActive(loading);
    }
    
    public void HideMessage()
    {
        MsgText.gameObject.SetActive(false);
    }



    
}
