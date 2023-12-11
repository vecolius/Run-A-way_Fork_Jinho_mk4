using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

namespace Jinho
{
    public class ItemShotgun : MonoBehaviour, IAttackItemable
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
        public Collider weaponCol;
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
            aimPos = player.Aim.aimObjPos;
            SetTransform(targetPosArray);
            //총알이 나가는 효과
            for(int i=0; i<targetPosArray.Length; i++)
            {
                GameObject bulletObj = Instantiate(bullet);
                Bullet bulletScript = bulletObj.GetComponent<Bullet>();
                bulletScript.SetBulletData(weaponData, Player);
                bulletScript.SetBulletVec(firePos, targetPosArray[i]);
            }
        }
        public void Reload()
        {
            if (BulletCount == maxBullet) return;
                BulletCount += 1;
        }
        public void SetItem(Player player)
        {
            if (player.weaponObjSlot[0] != null)
            {
                GameObject temp = player.weaponObjSlot[0];
                temp.transform.position = transform.position;
                temp.GetComponent<IAttackItemable>().Player = null;
                player.weaponObjSlot[0] = null;
                temp.SetActive(true);
            }
            this.player = player;
            player.weaponObjSlot[0] = gameObject;
            //player.weaponObjSlot[0].SetActive(false);
            weaponCol.enabled = false;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player) && this.player == null)
            {
                Debug.Log("asdf");
                SetItem(player);
            }
        }
    }
}
