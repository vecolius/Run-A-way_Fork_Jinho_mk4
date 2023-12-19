using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yeseul;
using Photon.Pun;
using Photon.Realtime;

namespace Jinho
{
    public class Ammo : MonoBehaviour, IInteractive
    {
        public void Interaction(GameObject interactivePlayer)
        {
            Debug.Log("√—æÀ¿Ã¥Ÿ~");
            Supply(interactivePlayer.GetComponent<Player>());
        }

        [PunRPC]
        void Supply(Jinho.Player player)
        {
            GameObject[] slots = player.weaponObjSlot;
            for(int i=0; i<slots.Length; i++)
            {
                if (slots[i] == null) continue;
                if(slots[i].TryGetComponent(out WeaponMonoBehaviour weaponAmmo))
                {
                    weaponAmmo.BulletCount = 100;
                    weaponAmmo.TotalBullet = 1000;
                }
            }
        }
    }
}
