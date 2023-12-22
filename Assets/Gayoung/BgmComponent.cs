using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;


namespace LimJinho
{
    public class BgmComponent : MonoBehaviour
    {
        public AudioClip[] BGclip;
        [SerializeField] AudioSource gameSource;

        public void BgmPlay(int index)
        {
            if (gameSource.clip == BGclip[index])
                return;
            gameSource.clip = BGclip[index];
            gameSource.loop = true;
            gameSource.volume = 0.5f;
            gameSource.Play();

        }

    }

}


