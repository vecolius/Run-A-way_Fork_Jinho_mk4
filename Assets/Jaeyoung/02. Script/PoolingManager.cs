using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jaeyoung
{
    [System.Serializable]
    public class PoolingInfo
    {
        public PoolingComponent prefab;
        public int poolingSize;
    }

    public class ObjeactPooling
    {
        private Queue<GameObject> poolingQueue;
        private PoolingInfo poolingInfo;
        private GameObject parent;

        public ObjeactPooling(PoolingInfo poolingInfo, GameObject parent)
        {
            poolingQueue = new Queue<GameObject>();
            this.parent = parent;
            this.poolingInfo = poolingInfo;
            Add(poolingInfo.poolingSize);
        }

        public void Add(int size)
        {
            if (!PhotonNetwork.IsMasterClient)
                return;

            for(int i = 0; i < size; i++)
            {
                GameObject obj = PhotonNetwork.Instantiate(poolingInfo.prefab.name, parent.transform.position, parent.transform.rotation);
                obj.transform.SetParent(parent.transform);
                obj.SetActive(false);
                poolingQueue.Enqueue(obj);
            }
        }

        public GameObject Pop()
        {
            if (poolingQueue.Count <= 0)
                Add(poolingInfo.poolingSize / 3);

            GameObject popObj = poolingQueue.Dequeue();
            return popObj;
        }

        public void ReturnObj(GameObject obj)
        {
            obj.SetActive(false);
            obj.transform.SetParent(parent.transform);
            poolingQueue.Enqueue(obj);
        }
    }

    public enum PoolingType
    {
        BULLET,
        SOUND,
        ZOMBIE
    }


    // 재영이형이랑 상의 해서 리팩 해볼 것 
    // 재미는 있을 듯 
    // 내 생각에는 리플렉션 써서 type 에 해당하는 거
    // dictionary에 집어 넣어서 사용하는게 베스트
    // 그게 아니더라도 굳이 poolinginfo를 사용해야 하는 이유를
    // 잘 모르겠음
    public class PoolingManager : MonoBehaviour
    {
        public static PoolingManager instance;
        [SerializeField] private List<PoolingInfo> poolingInfoList = new List<PoolingInfo>();
        Dictionary<PoolingType, ObjeactPooling> poolingDic;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(this.gameObject);
        }

        private void Start()
        {
            poolingDic = new Dictionary<PoolingType, ObjeactPooling>();
            Init();
        }

        void Init()
        {
            foreach (var info in poolingInfoList)
            {
                GameObject objParent = new GameObject(info.prefab.name + "-Pool");
                objParent.transform.SetParent(this.transform);

                ObjeactPooling objectPool = new ObjeactPooling(info, objParent);
                poolingDic.Add(info.prefab.poolingType, objectPool);
            }
        }

        public GameObject PopObj(PoolingType type)
        {
            if (!poolingDic.ContainsKey(type))
                return null;

            return poolingDic[type].Pop();
        }

        public void ReturnPool(GameObject obj)
        {
            if (obj.TryGetComponent<PoolingComponent>(out PoolingComponent poolcom))
            {
                if (!poolingDic.ContainsKey(poolcom.poolingType))
                    return;

                poolingDic[poolcom.poolingType].ReturnObj(obj);
            }
        }
    }
}