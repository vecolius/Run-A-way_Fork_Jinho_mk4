using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaeyoung
{
    public class Search : Mission
    {
        [SerializeField] private GameObject targetItem;
        [SerializeField] private SpawnPoint spawnItemPoint;

        private void Start()
        {
            clearEvent.Invoke();
            targetItem.transform.position = spawnItemPoint.points[Random.Range(0, spawnItemPoint.points.Count)].position;
        }

        public override void Play()
        {
            base.Play();
        }
    }
}
