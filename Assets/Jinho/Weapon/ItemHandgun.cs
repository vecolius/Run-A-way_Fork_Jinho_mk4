using Jinho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    public class ItemHandgun : MonoBehaviour, IAttackItemable
    {
        public WeaponData weaponData;
        public WeaponData WeaponData { get { return weaponData; } }
        public Player Player { get => player; set { player = value; } }
        Player player = null;
        public Transform firePos;   //총알 발사 위치
        public GameObject bullet;   //날아갈 총알 GameObject
        Transform aimPos;           //총알이 날아갈 위치
        public ItemType ItemType => weaponData.itemType;
        public int maxBullet;       //장전되는 총알 양
        [SerializeField] int bulletCount;            //현재 총에 들어있는 총알 양
        public int BulletCount
        {
            get { return bulletCount; }
            set
            {
                bulletCount = value;
                if (bulletCount > maxBullet) bulletCount = maxBullet;
                if (bulletCount < 0) bulletCount = 0;
            }
        }
        [SerializeField] int maxTotalBullet;         //최대로 내가 가지고 있는 총알의 합계
        [SerializeField] int totalBullet;            //내가 가지고 있는 총알의 합계
        public int TotalBullet
        {
            get { return totalBullet; }
            set
            {
                totalBullet = value;
                if (totalBullet > maxTotalBullet) totalBullet = maxTotalBullet;
                if (totalBullet < 0) totalBullet = 0;
            }
        }
        public void Use()
        {
            /*
            if (weaponData.BulletCount == 0)
                return;
            weaponData.BulletCount--;
            */
            //총알이 나가는 효과
            //이펙트 + 사운드
            aimPos = player.Aim.aimObjPos;
            GameObject bulletObj = Instantiate(bullet);
            Bullet bulletScript = bulletObj.GetComponent<Bullet>();
            bulletScript.SetBulletData(weaponData, Player);
            bulletScript.SetBulletVec(firePos, aimPos.position);
        }
        public void Reload()
        {
            int needBulletCount = maxBullet - BulletCount;

            if (TotalBullet >= needBulletCount)
                BulletCount = maxBullet;
            else
                BulletCount += TotalBullet;

            TotalBullet -= needBulletCount;
        }
        public void SetItem(Player player)
        {
            if (player.weaponObjSlot[1] != null)
            {
                GameObject temp = player.weaponObjSlot[0];
                temp.transform.position = transform.position;
                temp.GetComponent<IAttackItemable>().Player = null;
                player.weaponObjSlot[1] = null;
                temp.SetActive(true);
            }
            this.player = player;
            player.weaponObjSlot[1] = gameObject;
            //player.weaponObjSlot[1].SetActive(false);
        }
        private void OnTriggerEnter(Collider other)
        {
            Debug.Log(other.name);
            if (other.TryGetComponent(out Player player) && this.player == null)
            {
                SetItem(player);
            }
        }
    }
}
