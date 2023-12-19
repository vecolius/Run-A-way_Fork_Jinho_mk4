using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yeseul
{
    public class Sound : MonoBehaviour //소리 프리펩이 가져야함.
    {
        AudioSource m_AudioSource;
        
        
        private void Start()
        {
            m_AudioSource = GetComponent<AudioSource>();
            m_AudioSource.clip = AudioManager.instance.SetAudioSource(gameObject);
            m_AudioSource.Play();
            Debug.Log("(대충 효과음)");
        }

        private void Update()
        {
            if(m_AudioSource.isPlaying == false)
            {
                Debug.Log("재생 끝, 반납!");
                PoolingManager.instance.ReturnPool(this.gameObject);
            }
        }

    }

}
