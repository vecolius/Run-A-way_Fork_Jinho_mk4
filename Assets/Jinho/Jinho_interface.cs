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

}
