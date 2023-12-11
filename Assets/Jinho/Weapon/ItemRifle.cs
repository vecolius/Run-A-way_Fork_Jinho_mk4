using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    public class ItemRifle : MonoBehaviour, IAttackable
    {
        public WeaponData weaponData;
        public WeaponData WeaponData {  get { return weaponData; } }
        public Transform firePos;   //총알 발사 위치
        public GameObject bullet;   //날아갈 총알 GameObject
        Transform aimPos;           //총알이 날아갈 위치
        public ItemType ItemType => weaponData.itemType;

        public void Use()
        {
            /*
            if (weaponData.BulletCount == 0)
                return;
            weaponData.BulletCount--;
            
            //총알이 나가는 효과
            GameObject bulletObj = Instantiate(bullet);
            bulletObj.GetComponent<bullet>().SetBulletData(weaponData);
            bulletObj.GetComponent<bullet>().SetBulletVec(firePos, aimPos.position);
            */
            //총알이 나가는 효과
            //이펙트 + 사운드


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
        public void SetItem(PlayerController player)
        {
            if (player.weaponObjSlot[0] != null)
            {
                GameObject temp = player.weaponObjSlot[0];
                temp.transform.position = transform.position;
                temp.GetComponent<IAttackable>().WeaponData.player = null;
                player.weaponObjSlot[0] = null;
                temp.SetActive(true);
            }
            weaponData.player = player;
            player.weaponObjSlot[0] = gameObject;
            player.weaponObjSlot[0].SetActive(false);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out PlayerController player) && weaponData.player == null)
            {
                SetItem(player);
            }
        }

    }
}
