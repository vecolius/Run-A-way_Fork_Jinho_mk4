using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Jinho;

public class PoolManager : MonoBehaviour
{
    
    public static PoolManager instance;
    [SerializeField]
    GameObject parent;
    Type cachingType;
    GameObject createObj;
    GameObject item;

    public const int minSize = 10;

    private void Awake()
    {
        if(instance == null)
            instance = this;
        else
            Destroy(this.gameObject);
    
        poolDict = new Dictionary<Type, Queue<GameObject>>();
    }

    
    Dictionary<Type, Queue<GameObject>> poolDict;


    public void PoolAddItem(IPoolingItemAble makeListType )
    {
        poolDict[makeListType.GetItemType()] = new Queue<GameObject>();
    }

    public void ReturnPool( IPoolingItemAble item )
    {
        Type itemType = item.GetItemType();
        GameObject itemObject = item.GetGameObj();

        if (!poolDict.ContainsKey(itemType))
            PoolAddItem(item);
        
        itemObject.SetActive(false);
        poolDict[itemType].Enqueue(itemObject);
    }

    public GameObject PopPool( String typeName )
    {
        foreach (var item in poolDict.Keys)
        {
            if(item.Name == typeName)
                return poolDict[item].Dequeue();
        }

        return null;
    }

    public GameObject PopPool( IPoolingItemAble itemType )
    {

        cachingType = itemType.GetType();
        
        if (poolDict.ContainsKey(cachingType))
        {
            while ( poolDict[cachingType].Count <= 0)
            {
                createObj = Instantiate(itemType.GetGameObj(), parent.transform);
                createObj.SetActive(false);
                poolDict[cachingType].Enqueue(createObj);
            }
        }
        else
        {
            PoolAddItem(itemType);
            for (int i = 0; i< minSize; i++)
            {
                createObj = Instantiate(itemType.GetGameObj(), parent.transform);
                createObj.SetActive(false);
                poolDict[cachingType].Enqueue(createObj);
            }
        }

        item = poolDict[cachingType].Dequeue();
        item.SetActive(true);

        return item;
    }


}
