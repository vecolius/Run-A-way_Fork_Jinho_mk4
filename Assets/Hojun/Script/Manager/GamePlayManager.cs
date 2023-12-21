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
                        return false;
                }


                return true;
            }
        }



        public void Start()
        {
            StartCoroutine(WaitForDeadEnd());
        }

        public void Update()
        {
            Debug.Log(players.Count);
        }

        IEnumerator WaitForDeadEnd()
        {
            yield return new WaitUntil( () => IsPlayerDead );
            Debug.Log("Game End Scene Call");
        }






    }
}