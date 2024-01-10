using Jaeyoung;
using Jinho;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hojun
{


    public class GamePlayManager : SingleTon<GamePlayManager>
    {
    
        public List<Player> players = new List<Player>();

        public event Action DeadEndSceneCall;
        public event Action GameClearCall;

        public bool IsGameClear 
        {
            get => MissionManager.instance.isEnding;
        }

        public bool IsPlayerDead 
        {
            get
            {
                bool playerIsMine;
                foreach (var player in players) 
                {
                    playerIsMine = player.GetComponent<PhotonView>().IsMine;

                    if ( playerIsMine && player.IsDead )
                        return true;
                }

                return true;
            }
        }

        public void Start()
        {
            StartCoroutine(WaitForDeadEnd());
            StartCoroutine(WaitForGameClear());
        }

        IEnumerator WaitForDeadEnd()
        {
            yield return new WaitUntil( () => IsPlayerDead );
            DeadEndSceneCall();
            SceneController.instance.LoadScene("GameEnd");
            SceneController.instance.GameOverImage(0);
        }

        IEnumerator WaitForGameClear()
        {
            yield return new WaitUntil(() => IsGameClear);
            GameClearCall();
            SceneController.instance.LoadScene("GameEnd");
            SceneController.instance.GameOverImage(1);
        }


    }
}