using Jinho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaeyoung
{
    public class UIManager : DontDestroySingle<UIManager>
    {
        [SerializeField] private MinimapCam minimapCam;
        [SerializeField] private PlayerUI myUI;
        [SerializeField] private GameObject gameUI;
        public MissionUI missionUI;
        [SerializeField] private List<PlayerUI> anotherPlayerUI = new List<PlayerUI>();
        private int anotherPlayerCount = 0;

        public void PlayerEnter(Player player)
        {
            if (player.photonView.IsMine)
            {
                myUI.Player = player;
                gameUI.gameObject.SetActive(true);
                minimapCam.target = player.gameObject;
            }
            else
            {
                anotherPlayerUI[anotherPlayerCount].Player = player;
                anotherPlayerCount++;
            }
        }

        public void MissionUpdate(string title, string content)
        {
            missionUI.title.text = title;
            missionUI.content.text = content;
        }
    }
}