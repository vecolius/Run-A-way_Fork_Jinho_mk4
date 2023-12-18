using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestSound : MonoBehaviour
{
    AudioSource source;
    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if(source.isPlaying == false)
        {
            PoolingManager.instance.ReturnPool(gameObject);
        }
    }
}
