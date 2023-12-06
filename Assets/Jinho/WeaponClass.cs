using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Jinho_Weapon
{
    public abstract class Weapon
    {
        public Sprite image;        //총기 종류 이미지
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
        public Weapon(Sprite image, float damage, int maxBullet, int bulletCount, int maxTotalBullet, int totalBullet, Transform firePos, GameObject bullet)
        {
            this.image = image;
            this.damage = damage;
            this.maxBullet = maxBullet;
            BulletCount = bulletCount;
            this.maxTotalBullet = maxTotalBullet;
            TotalBullet = totalBullet;
            this.firePos = firePos;
            this.bullet = bullet;
        }

        public abstract void Fire();
        public abstract void Reload();
    }
    public class Rifle : Weapon
    {
        public Rifle(Sprite image, float damage, int maxBullet, int bulletCount, int maxTotalBullet, int totalBullet, Transform firePos, GameObject bullet) : base(image, damage, maxBullet, bulletCount, maxTotalBullet, totalBullet, firePos, bullet)
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
        public Shotgun(Sprite image, float damage, int maxBullet, int bulletCount, int maxTotalBullet, int totalBullet, Transform firePos, GameObject bullet) : base(image, damage, maxBullet, bulletCount, maxTotalBullet, totalBullet, firePos, bullet)
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
        public Handgun(Sprite image, float damage, int maxBullet, int bulletCount, int maxTotalBullet, int totalBullet, Transform firePos, GameObject bullet) : base(image, damage, maxBullet, bulletCount, maxTotalBullet, totalBullet, firePos, bullet)
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
        public Weapon weapon = null;

        public Sprite image;               //총기 종류 이미지
        public float damage;               //총 대미지
        public int maxBullet;              //장전되는 총알 양
        public int bulletCount;            //현재 총에 들어있는 총알 양
        public int maxTotalBullet;         //최대로 내가 가지고 있는 총알의 합계
        public int totalBullet;            //내가 가지고 있는 총알의 합계
        public Transform firePos;          //총알 발사 위치
        public GameObject bullet;          //날아갈 총알 GameObject
        void Start()
        {
            SetWeapon();
        }
        void SetWeapon()
        {
            switch (weaponType)
            {
                case WeaponType.Rifle:
                    weapon = new Rifle(image, damage, maxBullet, bulletCount, maxTotalBullet, totalBullet, firePos, bullet);
                    break;
                case WeaponType.Shotgun:
                    weapon = new Rifle(image, damage, maxBullet, bulletCount, maxTotalBullet, totalBullet, firePos, bullet);
                    break;
                case WeaponType.Handgun:
                    weapon = new Rifle(image, damage, maxBullet, bulletCount, maxTotalBullet, totalBullet, firePos, bullet);
                    break;
            }
        }
    }
}
