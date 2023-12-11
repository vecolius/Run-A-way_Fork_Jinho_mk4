using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Jaeyoung
{
    public class SpawnPoint : MonoBehaviour
    {
        public List<Transform> points = new List<Transform>();

        private void Start()
        {
            for(int i = 0; i< transform.childCount; i++)
            {
                points.Add(transform.GetChild(i));
            }
        }
    }
}
