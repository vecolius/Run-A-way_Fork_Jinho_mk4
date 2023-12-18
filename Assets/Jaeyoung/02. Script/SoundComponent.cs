using Hojun;
using Photon.Pun;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaeyoung
{
    // 사운드 발생시 범위체크
    public class SoundComponent : MonoBehaviourPunCallbacks
    {
        public float soundAreaSize;
        public float time;
        public event Action soundAction;



        public override void OnEnable()
        {
            base.OnEnable();
            soundAction += ActiveSound;
        }


        public void Update()
        {

            if (Input.GetKey(KeyCode.A))
            {
                //photonView.RPC("ActiveSound", RpcTarget.All);
            }
        }



        [PunRPC]
        public void ActiveSound()
        {
            Collider[] coll = Physics.OverlapSphere(transform.position, soundAreaSize);
         
            if (coll.Length > 0)
            {
                foreach (Collider zombie in coll)
                {
                    if (zombie.TryGetComponent<IHearAble>(out IHearAble zom))
                        zom.Hear(this.gameObject);
                    
                        
                }
            }


        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, soundAreaSize);
        }
    }
}