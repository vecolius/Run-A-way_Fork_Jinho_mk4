using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Jinho
{
    public class ItemShotgun : MonoBehaviour, IAttackable
    {
        public WeaponData weaponData;
        public WeaponData WeaponData { get { return weaponData; } }
        public Transform firePos;   //총알 발사 위치
        public GameObject bullet;   //날아갈 총알 GameObject
        Transform aimPos;           //총알이 날아갈 위치
        public ItemType ItemType => weaponData.itemType;
        void SetTransform(Vector3[] array)   //삿건 전용 총알 9개가 가야할 죄표
        {
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = Random.insideUnitSphere * 1.0f + aimPos.position;    //aimPos에서 일정 구 범위 안의 랜덤 좌표로 저장
            }
        }
        public void Use()
        {
            /*
            if (weaponData.BulletCount == 0)
                return;
            weaponData.BulletCount--;
            */
            //이펙트 + 사운드
            Vector3[] targetPosArray = new Vector3[9];
            SetTransform(targetPosArray);
            //총알이 나가는 효과
            for(int i=0; i<targetPosArray.Length; i++)
            {
                GameObject bulletObj = Instantiate(bullet);
                bulletObj.GetComponent<Bullet>().SetBulletData(weaponData);
                bulletObj.GetComponent<Bullet>().SetBulletVec(firePos, targetPosArray[i]);
            }
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
