using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jinho
{
    public class Weapon
    {
        protected WeaponData weaponData;
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
        public Weapon(WeaponData weaponData)
        {
            this.weaponData = weaponData;
            this.name = weaponData.weaponName;
            this.image = weaponData.image;
            this.attackState = weaponData.attackState;
            this.damage = weaponData.damage;
            this.maxBullet = weaponData.maxBullet;
            BulletCount = weaponData.bulletCount;
            this.maxTotalBullet = weaponData.maxTotalBullet;
            TotalBullet = weaponData.totalBullet;
            this.firePos = weaponData.firePos;
            this.bullet = weaponData.bullet;
        }

        public virtual void Fire() { }
        public virtual void Reload() { }
    }
    public class Rifle : Weapon
    {
        public Rifle(WeaponData weaponData) : base(weaponData)
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
        public Shotgun(WeaponData weaponData) : base(weaponData)
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
        public Handgun(WeaponData weaponData) : base(weaponData)
        {
        }

        public override void Fire()
        {
            
        }
        public override void Reload()
        {
            
        }
    }
    public class Sword : Weapon
    {
        public Sword(WeaponData weaponData) : base(weaponData)
        {
        }
        public override void Fire()
        {
            
        }
    }
    public class WeaponData
    {
        public string weaponName;          //무기 이름
        public Sprite image;               //총기 종류 이미지
        public float damage;               //총 대미지
        public int maxBullet;              //장전되는 총알 양
        public int bulletCount;            //현재 총에 들어있는 총알 양
        public int maxTotalBullet;         //최대로 내가 가지고 있는 총알의 합계
        public int totalBullet;            //내가 가지고 있는 총알의 합계
        public Transform firePos;          //총알 발사 위치
        public GameObject bullet;          //날아갈 총알 GameObject
        public PlayerAttackState attackState;
        public WeaponData(string name, Sprite image, float damage, int maxBullet, int bulletCount, int maxTotalBullet, int totalBullet, Transform firePos, PlayerAttackState attackState,GameObject bullet)
        {
            weaponName = name;
            this.image = image;
            this.damage = damage;
            this.maxBullet = maxBullet;
            this.bulletCount = bulletCount;
            this.maxTotalBullet = maxTotalBullet;
            this.totalBullet = totalBullet;
            this.firePos = firePos;
            this.bullet = bullet;
            this.attackState = attackState;
        }
    }
    public class WeaponClass : MonoBehaviour
    {
        public enum WeaponType
        {
            Rifle,
            Shotgun,
            Handgun,
            Sword,
        }
        public WeaponType weaponType;
        public Weapon weapon = null;

        public WeaponData weaponData;
        void Awake()
        {
            SetWeapon();
        }
        void SetWeapon()
        {
            switch (weaponType)
            {
                case WeaponType.Rifle:
                    weapon = new Rifle(weaponData);
                    break;
                case WeaponType.Shotgun:
                    weapon = new Shotgun(weaponData);
                    break;
                case WeaponType.Handgun:
                    weapon = new Handgun(weaponData);
                    break;
                case WeaponType.Sword:
                    weapon = new Sword(weaponData);
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
