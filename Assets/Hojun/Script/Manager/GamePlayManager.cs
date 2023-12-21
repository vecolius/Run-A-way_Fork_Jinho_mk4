using Jinho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Hojun
{


    public class GamePlayManager : SingleTon<GamePlayManager>
    {
    
        public List<Player> players = new List<Player>();

        public bool IsPlayerDead 
        {
            get
            {
                foreach (var player in players) 
                {
                    if (player.IsDead)
                        return true;
                }

                return false;
            }
        }



        public void Start()
        {
            StartCoroutine(WaitForDeadEnd());
        }


        IEnumerator WaitForDeadEnd()
        {
            yield return new WaitUntil( () => IsPlayerDead );
            Debug.Log("Game End Scene Call");


        }


    }
}