using Jinho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    [SerializeField] float moveSpeed;
    public float damage;
    public Weapon parentWeapon = null;
    void Start()
    {
        Destroy(gameObject, 1.2f);
    }

    void Update()
    {
        BulletMoveing();
    }
    void BulletMoveing()
    {
        transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
    }
    public void SetBulletData(Weapon weapon)
    {
        parentWeapon = weapon;
        damage = parentWeapon.damage;
    }
    void OnTriggerEnter(Collider other)
    {
        
    }
}
