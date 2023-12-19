using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yeseul
{
    public class SoundManagerT : MonoBehaviour
    {
        public static SoundManagerT instance = null;
        public GameObject soundSource = null; //소리 오브젝트
        AudioClip audioClip = null;

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


        public void SoundPlayT(GameObject obj, AudioClip clip, bool isLoop = false)
        {
            GameObject popobj = PoolingManager.instance.PopObj(PoolingType.SOUND);
            AudioSource source = obj.GetComponent<AudioSource>();
            source.clip = clip;
            source.loop = isLoop;
            popobj.transform.position = soundSource.transform.position;
            source.Play();
            
        }

        public void SoundPlay() //플레이어 이벤트 함수 
        {
            audioClip = AudioManager.instance.SetAudioSource(soundSource);
            SoundPlayT(soundSource, audioClip);

        }

    }
}
