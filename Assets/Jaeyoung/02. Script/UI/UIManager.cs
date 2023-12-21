using Jinho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaeyoung
{
    public class UIManager : DontDestroySingle<UIManager>
    {
        [SerializeField] private PlayerUI myUI;
        [SerializeField] private GameObject gameUI;
        [SerializeField] private List<PlayerUI> anotherPlayerUI = new List<PlayerUI>();
        private int anotherPlayerCount = 0;

        public void PlayerEnter(Player player)
        {
            if (player.photonView.IsMine)
            {
                myUI.Player = player;
                gameUI.gameObject.SetActive(true);
            }
            else
            {
                anotherPlayerUI[anotherPlayerCount].Player = player;
                anotherPlayerCount++;
            }
        }
    }
}