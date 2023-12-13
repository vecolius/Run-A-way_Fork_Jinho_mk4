using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho {
    public class ItemGrenade : MonoBehaviour, IAttackItemable
    {
        public WeaponData weaponData;
        public WeaponData WeaponData { get => weaponData; }
        public Player Player { get => player; set { player = value; } }
        Player player = null;
        public GameObject grenade;
        public float explosionRange;        //폭발 범위
        public ItemType ItemType { get => weaponData.itemType; }
        Vector3 endPos, startPos;           //날아갈 위치
        public int maxBullet;       //장전되는 총알 양
        int bulletCount;            //현재 총에 들어있는 총알 양
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
        int maxTotalBullet;         //최대로 내가 가지고 있는 총알의 합계
        int totalBullet;            //내가 가지고 있는 총알의 합계
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
            
            if (BulletCount == 0)
                return;
            BulletCount--;
            
            endPos = player.Aim.aimObjPos.position;
            GameObject bulletObj = Instantiate(grenade);
            bulletObj.GetComponent<Grenade>().SetGrenadeData(transform.position, endPos, player, explosionRange, weaponData.damage);
        }
        public void Reload()    //수류탄은 reload 없음
        {
            return;
        }
        public void SetItem(Player player)
        {
            WeaponItem.SetWeapon(player, gameObject, 3, this.player);
        }
        
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player) && this.player == null)
            {
                SetItem(player);
            }
        }
    }
}
