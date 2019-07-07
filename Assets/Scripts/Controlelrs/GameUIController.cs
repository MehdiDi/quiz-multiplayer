using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using Photon.Realtime;
using ExitGames.Client.Photon;
using Photon.Pun;

public class GameUIController : MonoBehaviour {

    public static GameUIController instance;

    [SerializeField]
    private Image QuestionImage;
    [SerializeField]
    private Text QuestionText;
    [SerializeField]
    private Button Answer1;
    [SerializeField]
    private Button Answer2;
    [SerializeField]
    private Button Answer3;

 

    [SerializeField]
    private Button Answer4;

    [SerializeField]
    private Image player1;
    [SerializeField]
    private Image player2;
    [SerializeField]
    private Image player3;

    [SerializeField]
    private Button BuzzerButton;
    [SerializeField]
    private Text Timer;
    [SerializeField]
    private Text QuestionTime;

    private Text scorePlayer1;
    private Text scorePlayer2;
    private Text scorePlayer3;


    private readonly byte RequestAnswer = 0;
    private readonly byte RightAnswer = 1;
    private readonly byte WrongAnswer = 2;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        scorePlayer1 = player1.transform.GetChild(1).GetComponent<Text>();
        scorePlayer2 = player2.transform.GetChild(1).GetComponent<Text>();
        //scorePlayer3 = player3.transform.GetChild(1).GetComponent<Text>();

        EnableAnswers(false);
        EnableBuzzer(false);
        
    }

    public void EnableAnswers(bool enabled)
    {
        Answer1.enabled = enabled;
        Answer2.enabled = enabled;
        Answer3.enabled = enabled;
        Answer4.enabled = enabled;

 
    }

    public void ActivateAnswers(bool active)
    {
        Answer1.gameObject.SetActive(active);
        Answer2.gameObject.SetActive(active);
        Answer3.gameObject.SetActive(active);
        Answer4.gameObject.SetActive(active);

        if (!active)
        {
            QuestionText.gameObject.SetActive(false);
            QuestionImage.gameObject.SetActive(false);
        }
    }

    public void ShowQuestion(Question question, List<Answer> answers)
    {
        StopAllCoroutines();
        StartCoroutine(QuestionTimer());

        if (question.IsImage)
        {
            QuestionText.gameObject.SetActive(false);
            QuestionImage.gameObject.SetActive(true);
            StartCoroutine(LoadImage(QuestionImage, question.Description));
        }
        else
        {
            QuestionImage.gameObject.SetActive(false);
            QuestionText.gameObject.SetActive(true);
            QuestionText.text = question.Description;
        }

        ActivateAnswers(true);

        Answer1.transform.GetChild(0).GetComponent<Text>().text = answers[0].Description;
        Answer1.tag = answers[0].IsTrue ? "true_answer" : "Untagged";

        Answer2.transform.GetChild(0).GetComponent<Text>().text = answers[1].Description;
        Answer2.tag = answers[1].IsTrue ? "true_answer" : "Untagged";

        Answer3.transform.GetChild(0).GetComponent<Text>().text = answers[2].Description;
        Answer3.tag = answers[2].IsTrue ? "true_answer" : "Untagged";

        Answer4.transform.GetChild(0).GetComponent<Text>().text = answers[3].Description;
        Answer4.tag = answers[3].IsTrue ? "true_answer" : "Untagged";

    }

    public static IEnumerator LoadImage(Image image, string url)
    {
        WWW www = new WWW(url);

        yield return www;
        
        www.LoadImageIntoTexture(image.mainTexture as Texture2D);
        www = null;


    }

    public void UpdateScore(string playerName, int score)
    {
        int playerNumber = GetPlayerNumber(playerName);

        Debug.Log(playerNumber + " " + playerName);

        switch (playerNumber)
        {
            case 1:
                scorePlayer1.text = score.ToString();
                break;
            case 2:
                scorePlayer2.text = score.ToString();

                break;
            case 3:
                scorePlayer3.text = score.ToString();
                break;
        }
    }

    public void PreparePlayers(Player[] players)
    {
        player1.transform.GetChild(0).GetComponent<Text>().text = players[0].NickName;
        player2.transform.GetChild(0).GetComponent<Text>().text = players[1].NickName;
        //player3.transform.GetChild(0).GetComponent<Text>().text = players[2].NickName;
    }

    public void OnBuzzerClick()
    {
        
        EnableBuzzer(false);
        

        object[] content = new object[] { PlayerPrefs.GetString("PlayerName") };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All};
        SendOptions sendOptions = new SendOptions { Reliability = true };
        PhotonNetwork.RaiseEvent(RequestAnswer, content, raiseEventOptions, sendOptions);
        
    }

    public void EnableBuzzer(bool enabled)
    {
        BuzzerButton.enabled = enabled;
    }
        
    public int GetPlayerNumber(string playerName)
    {
        if (player1.transform.GetChild(0).GetComponent<Text>().text == playerName)
            return 1;
        if (player2.transform.GetChild(0).GetComponent<Text>().text == playerName)
            return 2;

        return 3;
    }

    public void OnAnswerClick(Button button)
    {
        
        EnableAnswers(false);
        StopCoroutine(AnswerTimer());
        Timer.text = "";
        Timer.gameObject.SetActive(false);

        RaiseAnswerEvent(button.tag == "true_answer" ? RightAnswer : WrongAnswer);

    }

    public void RaiseAnswerEvent(byte code)
    {
        object[] content = new object[] { PlayerPrefs.GetString("PlayerName") };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        SendOptions sendOptions = new SendOptions { Reliability = true };

        PhotonNetwork.RaiseEvent(code, content, raiseEventOptions, sendOptions);
    }

    public void StartAnswerTimer()
    {
        Timer.gameObject.SetActive(true);

        StartCoroutine(AnswerTimer());
    }

    private IEnumerator AnswerTimer()
    {
        float time = GameSettings.instance.AnswerTime;

        do
        {
            time -= Time.deltaTime;
            Timer.text = Math.Round(time, 1).ToString();
            yield return null;

        } while (time >= 0);

        RaiseAnswerEvent(WrongAnswer);
    }

    private IEnumerator QuestionTimer()
    {
        float time = GameSettings.instance.QuestionTime;

        do
        {
            time -= Time.deltaTime;

            QuestionTime.text = ((int)time).ToString();

            yield return null;
        } while (time >= 0);
    }

}
