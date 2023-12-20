using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Jinho;
using System;

namespace Hojun
{


    public class CharacterUiModel : MonoBehaviour
    {
        public List<UiView> uiList = new List<UiView>();

        public Player Player 
        {
            set
            {
                player = value;
            }
        }
        Player player;

        public PlayerData GetData
        {
            get
            {
                return player.state;
            }
        }
        public void AddEvent(Action addEvent )
        {
            player.initList += addEvent;
        }


        public void Start()
        {
            


        }


    }


}