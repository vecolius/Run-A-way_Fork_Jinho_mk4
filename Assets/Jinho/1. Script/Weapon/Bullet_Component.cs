using Hojun;
using Jaeyoung;
using Jinho;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class Bullet_Component : MonoBehaviour, Hojun.IAttackAble
{
    public GameObject[] bulletEffect;
    [SerializeField] float moveSpeed;
    public float damage;
    public WeaponData parentWeaponData = null;
    public Jinho.Player player = null;
    Action<IHitAble> attackAction;

    Hojun.IHitAble target;

    void OnEnable()
    {
        Invoke("BulletDestroy", 1.2f);  //1.2sec -> return ObjectPool
    }
    void Start()
    {
        attackAction += BulletAttack;
    }
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    void BulletDestroy()    //return ObjectPool
    {
        PoolingManager.instance.ReturnPool(gameObject);
    }
    public void SetBulletData(WeaponData weaponData, Jinho.Player player)    //weaponData setting
    {
        this.player = player;
        parentWeaponData = weaponData;
        damage = parentWeaponData.damage;
    }
    public void SetBulletVec(Transform firePos, Vector3 targetPos)  //Bullet pos, rot, vec setting
    {
        transform.position = firePos.position;
        transform.rotation = firePos.rotation;
        transform.forward = (targetPos - transform.position).normalized;
    }
    void BulletAttack(IHitAble hitObj)
    {
        Debug.Log("damage");
        hitObj.Hit(GetDamage(), this);
    }

    public GameObject GetAttacker()
    {
        return player.gameObject;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Bullet_Component>() != null) return;

        if(other.GetComponent<IHitAble>() != null)
        {
            InstantiateEffect(1);
        }
        else
        {
            InstantiateEffect(0);
        }
        BulletDestroy();

    }
    [PunRPC]
    void InstantiateEffect(int index)
    {
        Instantiate(bulletEffect[index], transform.position, transform.rotation);
    }
    public void Attack()
    {
        attackAction(target);
    }

    public float GetDamage()
    {
        return parentWeaponData.damage;
    }
}
