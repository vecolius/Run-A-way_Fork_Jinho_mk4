using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    public class ItemRifle : MonoBehaviour, IAttackable
    {
        public WeaponData weaponData;
        public Transform firePos;   //총알 발사 위치
        public GameObject bullet;   //날아갈 총알 GameObject

        public ItemType ItemType => weaponData.itemType;

        public void Use()
        {
            /*
            if (weaponData.BulletCount == 0)
                return;
            weaponData.BulletCount--;
            */
            //총알이 나가는 효과
            GameObject bulletObj = Instantiate(bullet);
            bulletObj.transform.position = firePos.position;
            bulletObj.transform.rotation = firePos.rotation;
        }
        public void Reload()
        {
            int needBulletCount = weaponData.maxBullet - weaponData.BulletCount;

            if (weaponData.TotalBullet >= needBulletCount)
                weaponData.BulletCount = weaponData.maxBullet;
            else
                weaponData.BulletCount += weaponData.TotalBullet;

            weaponData.TotalBullet -= needBulletCount;
        }
        public void SetItem()
        {
            throw new System.NotImplementedException();
        }

        private void OnTriggerEnter(Collider other)
        {
            
        }

    }
}
