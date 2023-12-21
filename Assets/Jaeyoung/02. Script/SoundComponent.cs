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
        public event Action soundAction;
        [SerializeField] private AudioSource soundSource;

        private void Awake()
        {
            if (soundSource == null)
                return;

            soundAction += ActiveSound;
        }


        public override void OnEnable()
        {
            if (soundSource == null)
                return;

            base.OnEnable();
            soundSource.Play();
            soundAction();
        }


        public void Update()
        {
            if (soundSource == null)
                return;

            if (!soundSource.isPlaying)
                PoolingManager.instance.ReturnPool(this.gameObject);
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