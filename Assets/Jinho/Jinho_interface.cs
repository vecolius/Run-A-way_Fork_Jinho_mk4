using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hojnun;
using Hojun;
using Gayoung;

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
        public void SetItem(Player player);
    }
    public interface IAttackItemable : IUseable
    {
        public WeaponData WeaponData { get; }

    }
    public interface IExpendable : IUseable
    {
        public ExtendableData ExtendableData { get; }
    }
    #endregion
    #region Weapon_Class
    public class WeaponItem
    {
        public static void SetWeapon(Player player, GameObject weaponObj, int slotIndex, IAttackItemable attackItemable)
        {
            if (player.weaponObjSlot[slotIndex] != null)
            {

                GameObject temp = player.weaponObjSlot[slotIndex];
                Vector3 tempPos = weaponObj.transform.position;

                player.weaponObjSlot[slotIndex] = weaponObj;
                temp.GetComponent<IAttackItemable>().Player = null;

                temp.transform.position = tempPos;
                weaponObj.GetComponent<IAttackItemable>().Player = player;

                temp.SetActive(true);
                temp.GetComponent<Collider>().enabled = true;

                if (player.weaponIndex == slotIndex)   //플레이어가 슬롯의 무기를 들고있을 때,
                {
                    SetStrategy(player, weaponObj);
                    player.attackState = weaponObj.GetComponent<IAttackItemable>().ItemType;  //player가 그 슬롯의 무기를 들도록 설정
                    player.currentItemObj = player.weaponObjSlot[slotIndex];
                    player.currentItem = player.currentItemObj.GetComponent<IUseable>();
                }
                else                               //플레이어가 슬롯의 무기를 돌고있지 않을 때,
                    weaponObj.SetActive(false);
            
            }
            else
            {           //플레이어의 슬롯이 비었으면
                player.weaponObjSlot[slotIndex] = weaponObj;
                player.attackState = weaponObj.GetComponent<IAttackItemable>().ItemType;
                weaponObj.SetActive(false);
                weaponObj.GetComponent<IAttackItemable>().Player = player;
            }
            weaponObj.GetComponent<Collider>().enabled = false;
        }
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
    #endregion
}
