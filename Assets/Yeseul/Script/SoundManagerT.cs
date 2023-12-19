using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    public class SoundManagerT : MonoBehaviour
    {
        public SoundManagerT instance = null;
        public GameObject soundSource = null; //소리 낼 오브젝트
        //public Dictionary<GameObject, AudioClip> audioObj = new Dictionary<GameObject, AudioClip>(); 
        ////해당 GameObject 안에있는 AudioClip 가져올거니까 ? 
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
                Destroy(gameObject);
        }


        public void SoundPlay(GameObject obj, AudioClip clip, bool isLoop = false)
        {
            GameObject popobj = PoolingManager.instance.PopObj(PoolingType.SOUND);
            AudioSource source = obj.GetComponent<AudioSource>();
            source.clip = clip;
            source.loop = isLoop;
            popobj.transform.position = soundSource.transform.position;
            source.Play();
            
        }
    }
}
