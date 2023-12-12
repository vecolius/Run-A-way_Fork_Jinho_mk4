using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Jaeyoung
{
    public enum MissionType
    {
        Defense,
        Search,
        Breakthrough
    }

    public interface IMissionable
    {
        public void Play();
    }

    public abstract class Mission : MonoBehaviour, IMissionable
    {
        public string missiontName;
        public string missiontInfo;
        public MissionType missionType;
        [SerializeField] protected SpawnPoint spawnPoint;
        [SerializeField] private float spawnDelay;
        private float time;
        public UnityEvent clearEvent;


        private void Start()
        {
            clearEvent.Invoke();
        }

        public virtual void Play()
        {
            #region 좀비 소환
            if (time < spawnDelay)
            {
                time += Time.deltaTime;
                return;
            }

            time = 0;
            GameObject zombie = PoolingManager.instance.PopObj(PoolingType.ZOMBIE);
            zombie.transform.position = spawnPoint.points[UnityEngine.Random.Range(0, spawnPoint.points.Count)].position;
            zombie.SetActive(true);
            #endregion
        }
    }
}
