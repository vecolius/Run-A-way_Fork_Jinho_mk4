using Jinho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaeyoung
{
    public class UIManager : DontDestroySingle<UIManager>
    {
        [SerializeField] private GameObject minimapCam;
        [SerializeField] private PlayerUI myUI;
        [SerializeField] private GameObject gameUI;
        [SerializeField] private List<PlayerUI> anotherPlayerUI = new List<PlayerUI>();
        private int anotherPlayerCount = 0;
        [SerializeField] private float camDistance = 0;

        public void PlayerEnter(Player player)
        {
            if (player.photonView.IsMine)
            {
                myUI.Player = player;
                gameUI.gameObject.SetActive(true);
                minimapCam.transform.SetParent(player.transform);
                minimapCam.transform.localPosition = Vector3.up * camDistance;
                minimapCam.transform.LookAt(player.transform);
                minimapCam.transform.localRotation = Quaternion.Euler(new Vector3(90, 0, 0));
            }
            else
            {
                anotherPlayerUI[anotherPlayerCount].Player = player;
                anotherPlayerCount++;
            }
        }
    }
}