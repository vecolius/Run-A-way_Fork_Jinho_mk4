using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Hojnun;
using Hojun;

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
        public Player Player { get; set; }
        void Use();
        void Reload();
        void SetItem(Player player);
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
        public static void SetWeapon(Player player, GameObject weaponObj, int slotIndex, Player weaponDataPlayer)
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
                    player.attackState = weaponObj.GetComponent<IAttackItemable>().ItemType;  //player가 그 슬롯의 무기를 들도록 설정
                    player.weapon = player.weaponObjSlot[slotIndex];
                }
                else                               //플레이어가 슬롯의 무기를 돌고있지 않을 때,
                    temp.SetActive(false);
            
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
    }
    #endregion
}
