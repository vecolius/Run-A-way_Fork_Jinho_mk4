using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    public class SoundManager : MonoBehaviour
    {
        public SoundManager instance = null;
        GameObject soundObj = null;
        private void Awake()
        {
            if (instance == null)
            {
                instance = this;
            }
            else
                Destroy(gameObject);
        }

        void ReturnPoolObj()
        {
            PoolingManager.instance.ReturnPool(soundObj);
        }
        public void ReturnSound(GameObject soundObj, float soundLength)
        {
            this.soundObj = soundObj;
            Invoke("ReturnPoolObj", soundLength);
        }
        public void SoundPlay(GameObject obj, AudioClip clip, bool isLoop = false)
        {
            AudioSource source = obj.GetComponent<AudioSource>();
            source.clip = clip;
            source.loop = isLoop;
            obj.SetActive(true);
        }
    }
}
