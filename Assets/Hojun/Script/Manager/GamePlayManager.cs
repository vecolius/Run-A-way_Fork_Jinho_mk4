using Jaeyoung;
using Jinho;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hojun
{


    public class GamePlayManager : SingleTon<GamePlayManager>
    {
    
        public List<Player> players = new List<Player>();

        public event Action gameEndSceneCall;
        public event Action gameClearCall;

        public bool IsGameClear 
        {
            get => MissionManager.instance.isEnding;
        }

        public bool IsPlayerDead 
        {
            get
            {
                foreach (var player in players) 
                {
                    if (!player.IsDead)
                        return false;
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
            SceneController.instance.LoadScene("GameEnd");
            SceneController.instance.GameOverImage(0);
        }

        IEnumerator WaitForGameClear()
        {
            yield return new WaitUntil(() => IsGameClear);
            SceneController.instance.LoadScene("GameEnd");
            SceneController.instance.GameOverImage(1);
        }


    }
}