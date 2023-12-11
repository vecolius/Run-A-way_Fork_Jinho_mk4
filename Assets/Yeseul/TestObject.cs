using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Yeseul
{
    public class TestObject : MonoBehaviour, IInteractive
    {
        public void Interaction()
        {
            Debug.Log(gameObject.name + "실행했다");
        }

    }

}
