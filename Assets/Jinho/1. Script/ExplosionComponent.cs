using Hojun;
using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class ExplosionComponent : MonoBehaviour
{
    public GameObject explosionEffect;//폭발 이펙트
    public AudioClip explosionClip;     //폭발 사운드

    public float explosionRange;        //폭발 범위
    public float damage;                //폭발 대미지
    public SphereCollider searchCol;

    public GameObject attacker;
    Hojun.IHitAble target;

    [PunRPC]
    public void Explosion(GameObject attacker = null) //폭발
    {
        this.attacker = attacker;
        transform.GetComponent<MeshRenderer>().enabled = false;
        //이펙트 발생
        GameObject effectObj = Instantiate(explosionEffect, transform.position, transform.rotation);
        effectObj.GetComponent<Effect>().SetData(attacker, damage);
        effectObj.SetActive(true);
        //사운드 발생
        /*
        GameObject soundObj = PoolingManager.instance.PopObj(Jaeyoung.PoolingType.SOUND);
        AudioSource audioSource = soundObj.GetComponent<AudioSource>();
        audioSource.clip = explosionClip;
        soundObj.SetActive(true);
        audioSource.Play();
        */

        //searchCol.radius = explosionRange;
        //searchCol.enabled = true;

        //Collider[] cols = Physics.OverlapSphere(transform.position, explosionRange);
        //if(cols.Length > 0)
        //{
        //    foreach(var col in cols)
        //    {
        //        if (col.TryGetComponent(out Hojun.IHitAble hitable))
        //        {
        //            target = hitable;
        //        }
        //    }
        //}
        Destroy(gameObject);
    }

    public float GetDamage()
    {
        return damage;
    }
}
