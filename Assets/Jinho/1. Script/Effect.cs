using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect : MonoBehaviour, Hojun.IAttackAble
{
    GameObject attacker = null;
    float damage;
    public GameObject GetAttacker()
    {
        return attacker;
    }
    public float GetDamage()
    {
        return damage;
    }
    public void SetData(GameObject attacker, float damage)
    {
        this.attacker = attacker;
        this.damage = damage;
    }

    void OnParticleTrigger()
    {
        Debug.Log("¿Ã∆Â∆Æ πﬂµø");
    }
}
