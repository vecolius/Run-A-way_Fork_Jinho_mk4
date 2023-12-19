using Hojun;
using Jaeyoung;
using Jinho;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet_Component : MonoBehaviour, Hojun.IAttackAble
{
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
        BulletDestroy();

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
