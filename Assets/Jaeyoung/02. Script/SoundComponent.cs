using Hojun;
using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;

namespace Jaeyoung
{
    // 사운드 발생시 범위체크
    public class SoundComponent : MonoBehaviour
    {
        public float soundAreaSize;
        public float time;
        public event Action soundAction;



        private void OnEnable()
        {
            soundAction += ActiveSound;
        }


        public void Update()
        {

            if (Input.GetKey(KeyCode.A))
            {
                soundAction();
            }
        }


        public void ActiveSound()
        {

            Collider[] coll = Physics.OverlapSphere(transform.position, soundAreaSize);

            if (coll.Length > 0)
            {
                foreach (Collider zombie in coll)
                {
                    if (zombie.TryGetComponent<IHearAble>(out IHearAble zom))
                    {
                        zom.Hear(this.gameObject);
                        Debug.Log("zombie on");
                    }
                        
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