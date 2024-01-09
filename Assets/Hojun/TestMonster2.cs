using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMonster2 : TestMonster, IPoolingItemAble
{

    static Type itemType;

    public override GameObject GetGameObj()
    {
        return this.gameObject;
    }

    public override Type GetItemType()
    {
        if(itemType == null)
            itemType = this.GetType();

        return itemType;
    }

    public override void InitItem()
    {

    }

    public override void ReturnPool()
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



public interface IPoolingItemAble
{
    public abstract System.Type GetItemType();
    public abstract GameObject GetGameObj();
    public abstract void InitItem();
    public abstract void ReturnPool();

}
