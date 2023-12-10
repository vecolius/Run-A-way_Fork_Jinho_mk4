using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    public class ItemShotgun : MonoBehaviour, IAttackable
    {
        public WeaponData weaponData;
        public Transform firePos;   //총알 발사 위치
        public GameObject bullet;   //날아갈 총알 GameObject
        public ItemType ItemType => weaponData.itemType;
        void SetTransform(Quaternion[] array)   //삿건 전용 총알 9개가 가야할 죄표
        {
            
        }
        public void Use()
        {
            /*
            if (weaponData.BulletCount == 0)
                return;
            weaponData.BulletCount--;
            */
            Quaternion[] firePosArray = new Quaternion[9];
            SetTransform(firePosArray);
            //총알이 나가는 효과
            GameObject bulletObj = Instantiate(bullet);
            bulletObj.transform.position = firePos.position;
            bulletObj.transform.rotation = firePos.rotation;
        }
        public void Reload()
        {
            if (weaponData.BulletCount == weaponData.maxBullet) return;
            weaponData.BulletCount += 1;
        }
        public void SetItem(PlayerController player)
        {
            if (player.weaponObjSlot[0] != null)
            {
                GameObject temp = player.weaponObjSlot[0];
                temp.transform.position = transform.position;
                temp.SetActive(true);
                player.weaponObjSlot[0] = null;
            }
            weaponData.player = player;
            player.weaponObjSlot[0] = gameObject;
            player.weaponObjSlot[0].SetActive(false);
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out PlayerController player))
            {
                SetItem(player);
            }
        }
    }
}
