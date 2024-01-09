using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster : MonoBehaviour , IPoolingItemAble
{
    static Type itemType;

    public virtual GameObject GetGameObj()
    {
        return this.gameObject;
    }

    public virtual Type GetItemType()
    {
        if(itemType == null)
            itemType = this.GetType();

        return itemType;
    }

    public virtual void InitItem()
    {

    }

    public virtual void ReturnPool()
    {
        Re_PoolManager.instance.ReturnPool(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Die();
        }
    }


    public void Die()
    {
        ReturnPool();
    }

}
