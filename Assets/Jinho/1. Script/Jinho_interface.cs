using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hojnun;
using Hojun;
using Gayoung;
using Photon.Pun;
using Photon.Realtime;
using Jaeyoung;

namespace Jinho
{
    #region Item_interface
    public enum ItemType
    {
        Rifle,
        Shotgun,
        Melee,
        Handgun,
        HealKit,
        Grenade,
    }
    public interface IUseable
    {
        public ItemType ItemType { get; }
        public Player Player { get; set; }
        public IAttackStrategy AttackStrategy { get; set; }
        public void Use();
        public void UseEffect();
        [PunRPC]
        public void SetItem(Player player);
    }
    public interface IAttackItemable : IUseable
    {
        public void Reloading();
        public void ReloadEffect();
    }
    public interface IExpendable : IUseable
    {
        public ExtendableData ExtendableData { get; }
    }
    #endregion
    #region Weapon_Class
    public class WeaponItem
    {
        
        public static void SetStrategy(Player player, GameObject weaponObj)
        {
            IAttackItemable attackItemable = weaponObj.GetComponent<IAttackItemable>();
            switch (attackItemable.ItemType)
            {
                case ItemType.Rifle:
                    attackItemable.AttackStrategy = new RifleAttackStrategy(player);
                    break;
                case ItemType.Shotgun:
                    attackItemable.AttackStrategy = new ShotGunStregy(player);
                    break;
                case ItemType.Handgun:
                    attackItemable.AttackStrategy = new HandgunAttackStrategy(player);
                    break;
                case ItemType.Melee:
                    attackItemable.AttackStrategy = new MeleeAttackStrategy(player);
                    break;
                case ItemType.HealKit:
                    //attackItemable.AttackStrategy = new HealKitAttackStrategy(player);
                    break;
                case ItemType.Grenade:
                    attackItemable.AttackStrategy = new GranadeAttackStrategy(player);
                    break;
            }
            player.attackStrategy = attackItemable.AttackStrategy;
        }
    }
    public class WeaponMonoBehaviour : MonoBehaviourPun
    {
        [SerializeField]protected WeaponData weaponData;
        public WeaponData WeaponData { get { return weaponData; } }

        [SerializeField]protected int maxTotalBullet;         //최대로 내가 가지고 있는 총알의 합계
        [SerializeField]protected int totalBullet;            //내가 가지고 있는 총알의 합계
        [SerializeField]protected int maxBullet;       //장전되는 총알 양
        [SerializeField]protected int bulletCount;            //현재 총에 들어있는 총알 양

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
        [PunRPC]
        protected void SoundEffect(AudioClip clip, Transform transform)    //소리 재생
        {
            GameObject soundObj = PoolingManager.instance.PopObj(PoolingType.SOUND);
            soundObj.transform.position = transform.position;
            AudioSource sound = soundObj.GetComponent<AudioSource>();
            sound.clip = clip;
            soundObj.SetActive(true);
            sound.Play();
        }
        [PunRPC]
        protected void InstantiateEffect(GameObject effectObj, Transform transform)
        {
            Instantiate(effectObj, transform.position, transform.rotation);
        }

        [PunRPC]
        public void SetWeapon(Player player, GameObject weaponObj, int slotIndex, IUseable attackItemable = null)
        {
            if (player.weaponObjSlot[slotIndex] != null)
            {

                GameObject temp = player.weaponObjSlot[slotIndex];
                Vector3 tempPos = weaponObj.transform.position;

                player.weaponObjSlot[slotIndex] = weaponObj;
                temp.GetComponent<IUseable>().Player = null;

                temp.transform.position = tempPos;
                weaponObj.GetComponent<IUseable>().Player = player;

                temp.SetActive(true);
                temp.GetComponent<Collider>().enabled = true;

                if (player.weaponIndex == slotIndex)   //플레이어가 슬롯의 무기를 들고있을 때,
                {
                    //SetStrategy(player, weaponObj);
                    player.attackState = weaponObj.GetComponent<IUseable>().ItemType;  //player가 그 슬롯의 무기를 들도록 설정
                    player.currentItemObj = player.weaponObjSlot[slotIndex];
                    player.currentItem = player.currentItemObj.GetComponent<IUseable>();
                }
                else                               //플레이어가 슬롯의 무기를 돌고있지 않을 때,
                {
                    weaponObj.SetActive(false);
                }

            }
            else
            {           //플레이어의 슬롯이 비었으면
                player.weaponObjSlot[slotIndex] = weaponObj;
                player.attackState = weaponObj.GetComponent<IUseable>().ItemType;
                weaponObj.SetActive(false);
                weaponObj.GetComponent<IUseable>().Player = player;
            }
            weaponObj.GetComponent<Collider>().enabled = false;
        }
    }
    #endregion
}
