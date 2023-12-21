using Jinho;
using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaeyoung
{
    public class GameManager : SingleTon<Jaeyoung.GameManager>
    {
        public MyCharacterUI myUI;

        public List<GameObject> currentPlayerList = new List<GameObject>();
        int count = 0;

        private void Start()
        {
            GameManager.instance.currentPlayerList.Add(this.gameObject);

            foreach(GameObject obj in GameManager.instance.currentPlayerList)
            {
                
                //if (myUI)
                

                PhotonView pv = obj.GetComponent<PhotonView>();
                Jinho.Player playerData = obj.GetComponent<Jinho.Player>();

                if (pv.IsMine)
                {
                    
                    // ≥ª UIø° ≥÷æÓ¡‹
                }
                else
                {
                    // ø∑ø° ≥÷æÓ¡‹
                }
            }
        }
    }
}