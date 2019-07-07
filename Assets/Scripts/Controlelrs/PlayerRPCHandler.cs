using Photon.Pun;
using Photon.Realtime;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using ExitGames.Client.Photon;

namespace Assets.Scripts.Controlelrs
{
    public class PlayerRPCHandler : MonoBehaviour, IOnEventCallback
    {

        public float StartDelay = 1.5f;
        [SerializeField]
        private PhotonView _photonView;

        private GPlayer _player;
        private bool _canAnswer = false;
        private bool _answered = false;


        private readonly byte RequestAnswer = 0;
        private readonly byte RightAnswer = 1;
        private readonly byte WrongAnswer = 2;

        private bool masterNext = false;

        public bool CanAnswer
        {
            get
            {
                return _canAnswer;
            }

            set
            {
                _canAnswer = value;
            }
        }

        private void Start()
        {
            _player = GetComponent<GPlayer>();

            if (_photonView.IsMine)
                _photonView.RPC("SetPlayer", RpcTarget.All, PlayerPrefs.GetString("PlayerName"));

            if (PhotonNetwork.IsMasterClient)
            {
                PreparePlayers();
                _photonView.RPC("PreparePlayers", RpcTarget.Others);
            }

        }

        public void OnEnable()
        {
            PhotonNetwork.AddCallbackTarget(this);
        }

        public void OnDisable()
        {
            PhotonNetwork.RemoveCallbackTarget(this);
        }

        [PunRPC]
        public void SetPlayer(string name)
        {
            if (_player == null)
                _player = GetComponent<GPlayer>();

            _player.PlayerName = name;
        }

 

        [PunRPC]
        public void PreparePlayers()
        {
            GameUIController.instance.PreparePlayers(PhotonNetwork.PlayerList);
            QuestionController.instance.LoadQuestions();

            

            StartCoroutine(WaitStart(StartDelay));
        }



        private IEnumerator WaitStart(float delay)
        {
            yield return new WaitForSeconds(delay);

            QuestionController.instance.Next();

            if (PhotonNetwork.IsMasterClient)
                QuestionController.instance.Next(0);

            
            CanAnswer = true;
        }

        public void OnEvent(EventData photonEvent)
        {
            if (!_photonView.IsMine)
                return;

            if (photonEvent.Code > 3)
                return;


            string playerName = ((object[])photonEvent.CustomData)[0] as string;
            
            switch (photonEvent.Code)
            {
                case 0: 

                    Player[] players = PhotonNetwork.PlayerList;

                    for (int i = 0; i < players.Length; i++)
                    {
                        if (playerName == PhotonNetwork.NickName)
                        {
                            if (CanAnswer)
                            {
                                
                                GameUIController.instance.EnableAnswers(true);
                                
                                GameUIController.instance.StartAnswerTimer();
                                _answered = true;
                                return;

                            }
                        }
                    }
                    CanAnswer = false;
                    GameUIController.instance.EnableBuzzer(false);
                    

                    break;
                case 1:

                    PhotonRoom.instance.AddScore(playerName, GameSettings.instance.AnswerPoints);
                    QuestionController.instance.QuestionIndex += 1;

                    int index = PhotonNetwork.IsMasterClient ? QuestionController.instance.QuestionIndex - 1 : QuestionController.instance.QuestionIndex;
                    QuestionController.instance.Next(index);


                    CanAnswer = true;

                    _answered = false;

                    break;
                case 2:
                    
                    if (_answered)
                        return;

                    GameUIController.instance.EnableBuzzer(true);

                    CanAnswer = true;

                    break;
            }
        }


    }
}