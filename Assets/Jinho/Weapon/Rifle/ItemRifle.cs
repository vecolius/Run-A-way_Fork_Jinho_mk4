using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    public class ItemRifle : MonoBehaviour, IAttackItemable, Yeseul.IInteractive
    {
        public WeaponData weaponData;
        public WeaponData WeaponData {  get { return weaponData; } }
        public Player Player {  get=> player; set { player = value; } }
        [SerializeField] Player player = null;
        public Transform firePos;   //총알 발사 위치
        public GameObject bullet;   //날아갈 총알 GameObject
        Transform aimPos;           //총알이 날아갈 위치
        public ItemType ItemType => weaponData.itemType;
        public int maxBullet;       //장전되는 총알 양
        [SerializeField]int bulletCount;            //현재 총에 들어있는 총알 양
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
        [SerializeField]int maxTotalBullet;         //최대로 내가 가지고 있는 총알의 합계
        [SerializeField]int totalBullet;            //내가 가지고 있는 총알의 합계
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
        public AudioClip gunFireSound;
        public void Use()
        {
            
            if (BulletCount == 0)
                return;
            BulletCount--;

            //이펙트 + 사운드
            GameObject soundObj = PoolingManager.instance.PopObj(PoolingType.SOUND);
            soundObj.transform.position = firePos.position;
            soundObj.GetComponent<AudioSource>().clip = gunFireSound;
            soundObj.SetActive(true);
            
            //총알이 나가는 효과
            aimPos = player.Aim.aimObjPos;
            GameObject bulletObj = PoolingManager.instance.PopObj(PoolingType.BULLET);
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
            WeaponItem.SetWeapon(player, gameObject, 0, this.player);
        }

        public void Interaction(GameObject interactivePlayer)
        {
            if (interactivePlayer.TryGetComponent(out Player player) && this.player == null)
            {
                SetItem(player);
            }
        }
        private void OnTriggerEnter(Collider other)
        {

        }
    }
}
