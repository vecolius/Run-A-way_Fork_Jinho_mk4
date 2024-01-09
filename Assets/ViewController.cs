using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hojun;

namespace Hojun
{

    public class ViewController : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            GamePlayManager.instance.GameClearCall += () => { gameObject.SetActive(false); };
        }

    }
}