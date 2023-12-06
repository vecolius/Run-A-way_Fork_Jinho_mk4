using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jinho
{
    public class Weapon
    {
        public string name;         //총기 이름
        public Sprite image;        //총기 종류 이미지
        public PlayerAttackState attackState;   //공격 방식
        public float damage;        //총 대미지
        public int maxBullet;       //장전되는 총알 양
        int bulletCount;            //현재 총에 들어있는 총알 양
        public int BulletCount
        {
            get { return bulletCount; }
            set 
            {
                bulletCount = value;
                if(bulletCount > maxBullet) bulletCount = maxBullet;
                if(bulletCount < 0 ) bulletCount = 0;
            }
        }
        int maxTotalBullet;         //최대로 내가 가지고 있는 총알의 합계
        int totalBullet;            //내가 가지고 있는 총알의 합계
        public int TotalBullet
        {
            get { return  totalBullet; }
            set 
            {
                totalBullet = value; 
                if(totalBullet > maxTotalBullet) totalBullet = maxTotalBullet;
                if(totalBullet < 0 ) totalBullet = 0;
            }
        }

        public Transform firePos;   //총알 발사 위치
        public GameObject bullet;   //날아갈 총알 GameObject
        public Weapon(string name, Sprite image, float damage, int maxBullet, int bulletCount, int maxTotalBullet, int totalBullet, Transform firePos, PlayerAttackState attackState, GameObject bullet = null)
        {
            this.name = name;
            this.image = image;
            this.attackState = attackState;
            this.damage = damage;
            this.maxBullet = maxBullet;
            BulletCount = bulletCount;
            this.maxTotalBullet = maxTotalBullet;
            TotalBullet = totalBullet;
            this.firePos = firePos;
            this.bullet = bullet;
        }

        public virtual void Fire() { }
        public virtual void Reload() { }
    }
    public class Rifle : Weapon
    {
        public Rifle(string name, Sprite image, float damage, int maxBullet, int bulletCount, int maxTotalBullet, int totalBullet, Transform firePos, PlayerAttackState attackState, GameObject bullet) : base(name, image, damage, maxBullet, bulletCount, maxTotalBullet, totalBullet, firePos, attackState, bullet)
        {
        }

        public override void Fire()
        {
            Debug.Log("라이플 빵!");
        }
        public override void Reload()
        {
            Debug.Log("라이플 재장전~");
        }
    }
    public class Shotgun : Weapon
    {
        public Shotgun(string name, Sprite image, float damage, int maxBullet, int bulletCount, int maxTotalBullet, int totalBullet, Transform firePos, PlayerAttackState attackState, GameObject bullet) : base(name, image, damage, maxBullet, bulletCount, maxTotalBullet, totalBullet, firePos, attackState, bullet)
        {
        }

        public override void Fire()
        {
            
        }
        public override void Reload()
        {

        }
    }
    public class Handgun : Weapon
    {
        public Handgun(string name, Sprite image, float damage, int maxBullet, int bulletCount, int maxTotalBullet, int totalBullet, Transform firePos, PlayerAttackState attackState, GameObject bullet) : base(name, image, damage, maxBullet, bulletCount, maxTotalBullet, totalBullet, firePos, attackState, bullet)
        {
        }

        public override void Fire()
        {
            
        }
        public override void Reload()
        {
            
        }
    }
    public class WeaponClass : MonoBehaviour
    {
        public enum WeaponType
        {
            Rifle,
            Shotgun,
            Handgun,
        }
        public WeaponType weaponType;
        public PlayerAttackState attackState;
        public Weapon weapon = null;

        public string weaponName;          //무기 이름
        public Sprite image;               //총기 종류 이미지
        public float damage;               //총 대미지
        public int maxBullet;              //장전되는 총알 양
        public int bulletCount;            //현재 총에 들어있는 총알 양
        public int maxTotalBullet;         //최대로 내가 가지고 있는 총알의 합계
        public int totalBullet;            //내가 가지고 있는 총알의 합계
        public Transform firePos;          //총알 발사 위치
        public GameObject bullet;          //날아갈 총알 GameObject
        void Awake()
        {
            SetWeapon();
        }
        void SetWeapon()
        {
            switch (weaponType)
            {
                case WeaponType.Rifle:
                    weapon = new Rifle(weaponName, image, damage, maxBullet, bulletCount, maxTotalBullet, totalBullet, firePos, attackState, bullet);
                    break;
                case WeaponType.Shotgun:
                    weapon = new Shotgun(weaponName, image, damage, maxBullet, bulletCount, maxTotalBullet, totalBullet, firePos, attackState, bullet);
                    break;
                case WeaponType.Handgun:
                    weapon = new Handgun(weaponName, image, damage, maxBullet, bulletCount, maxTotalBullet, totalBullet, firePos, attackState, bullet);
                    break;
            }
        }
        private void OnTriggerEnter(Collider other)
        {
            if(other.TryGetComponent(out PlayerController player))
            {
                player.weaponSlot[0] = weapon;
                player.currentWeapon = player.weaponSlot[0];
                gameObject.SetActive(false);
            }
        }
    }
}
