using Jinho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public float damage;
    public WeaponData parentWeaponData = null;
    public PlayerController player = null;
    void OnEnable()
    {
        Invoke("BulletDestroy", 1.2f);  //총알이 불러와지면 1.2초 뒤 스스로 파괴됨
    }
    void Update()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    void BulletDestroy()
    {
        Destroy(gameObject);
    }
    public void SetBulletData(WeaponData weaponData)    //무기 damage 입력 함수
    {
        this.player = weaponData.player;
        parentWeaponData = weaponData;
        damage = parentWeaponData.damage;
    }
    void OnTriggerEnter(Collider other)
    {
        //if(other.TryGetComponent(out IHitable hit)
    }
}
