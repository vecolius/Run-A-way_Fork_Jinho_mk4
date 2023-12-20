using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

namespace Yeseul
{
    public class SoundManagerT : MonoBehaviour
    {
        public static SoundManagerT instance = null;
        public AudioClip[] audioClips;
        public GameObject playerObj;

        //public Dictionary<GameObject, AudioClip> audioObj = new Dictionary<GameObject, AudioClip>(); 
        ////해당 GameObject 안에있는 AudioClip 가져올거니까 ? 
        private void Awake()
        {
            playerObj = transform.gameObject;
        }
        [PunRPC]
        public void SoundPlayT(AudioClip clip, bool isLoop = false)
        {
            GameObject popobj = PoolingManager.instance.PopObj(PoolingType.SOUND);
            AudioSource source = popobj.GetComponent<AudioSource>();
            source.clip = clip;
            source.loop = isLoop;
            popobj.transform.position = playerObj.transform.position;
            popobj.SetActive(true);
            source.Play();
            
        }
        [PunRPC]
        public void SoundPlay() //Animation Event 함수 (player 발걸음 소리 random) 
        {
            //audioClips = AudioManager.instance.SetAudioSource(soundSource);
            int index = Random.Range(0, 3);
            SoundPlayT(audioClips[index]);

        }

    }
}
