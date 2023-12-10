using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    #region Item_interface
    public enum ItemType
    {
        rifle,
        shotgun,
        Melee,
        Handgun,
        HealKit,
        Grenade,
    }
    public interface IUseable
    {
        public ItemType ItemType { get; }
        void Use();
        void Reload();
        void SetItem(PlayerController player);
    }
    public interface IAttackable : IUseable 
    {
        public WeaponData WeaponData { get; }
    }
    public interface IExpendable : IUseable { }
    #endregion

    #region ItemData_Class
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Object/Weapon Data", order = int.MaxValue)]
    public class WeaponData : ScriptableObject
    {
        public ItemType itemType;   //아이템 타입
        public string itemName;     //아이템 이름
        public Sprite image;        //아이템 이미지
        public float damage;        //총 대미지
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
        public Transform firePos;   //총알 발사 위치
        public GameObject bullet;   //날아갈 총알 GameObject
        public PlayerController player;
    }
    public class ExtendableData
    {
        public ItemType itemType;   //아이템 타입
        public string itemName;     //아이템 이름
        public Sprite image;        //아이템 이미지
        public float effectValue;   //아이템 효과수치
        public PlayerController player;
    }
    #endregion

    #region WeaponClass
    public class Rifle : IAttackable
    {
        WeaponData weaponData;
        public WeaponData WeaponData { get => weaponData; set { weaponData = value; } }

        public ItemType ItemType => throw new System.NotImplementedException();

        public void SetItem(PlayerController player)
        {
            throw new System.NotImplementedException();
        }

        public void Reload()
        {
            throw new System.NotImplementedException();
        }

        public void Use()
        {
            if (WeaponData.BulletCount == 0)
                return;
            WeaponData.BulletCount--;

        }
    }
    public class Shotgun : IAttackable
    {
        WeaponData weaponData;
        public WeaponData WeaponData { get => weaponData; set { weaponData = value; } }

        public ItemType ItemType => throw new System.NotImplementedException();

        public void SetItem(PlayerController player)
        {
            throw new System.NotImplementedException();
        }

        public void Reload()
        {
            throw new System.NotImplementedException();
        }

        public void Use()
        {
            if (WeaponData.BulletCount == 0)
                return;
            WeaponData.BulletCount--;
        }
    }
    public class Melee : IAttackable
    {
        WeaponData weaponData;
        public WeaponData WeaponData { get => weaponData; set { weaponData = value; } }

        public ItemType ItemType => throw new System.NotImplementedException();

        public void SetItem(PlayerController player)
        {
            throw new System.NotImplementedException();
        }

        public void Reload()
        {
            throw new System.NotImplementedException();
        }

        public void Use()
        {

        }
    }
    public class Handgun : IAttackable
    {
        WeaponData weaponData;
        public WeaponData WeaponData { get => weaponData; set { weaponData = value; } }

        public ItemType ItemType => throw new System.NotImplementedException();

        public void SetItem(PlayerController player)
        {
            throw new System.NotImplementedException();
        }

        public void Reload()
        {
            throw new System.NotImplementedException();
        }

        public void Use()
        {
            if (WeaponData.BulletCount == 0)
                return;
            WeaponData.BulletCount--;

        }
    }
    public class Grenade : IAttackable
    {
        public WeaponData weaponData;
        public WeaponData WeaponData => weaponData;
        public ItemType ItemType => throw new System.NotImplementedException();

        public void Reload()
        {
            throw new System.NotImplementedException();
        }

        public void SetItem(PlayerController player)
        {
            throw new System.NotImplementedException();
        }

        public void Use()
        {

        }
    }
    #endregion

    #region ExtendableItemClass
    public class HealKit : IExpendable
    {
        ExtendableData extendableData;
        public ExtendableData ExtendableData { get => extendableData; set {  extendableData = value; } }

        public ItemType ItemType => throw new System.NotImplementedException();

        public void Reload()
        {
            throw new System.NotImplementedException();
        }

        public void SetItem(PlayerController player)
        {
            throw new System.NotImplementedException();
        }

        public void Use() 
        {
            
        }
    }
    #endregion

    public class Item : MonoBehaviour
    {
        void Start()
        {

        }

        void Update()
        {

        }
    }
}
