using Jinho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
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
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Object/Weapon Data", order = int.MaxValue)]
    public class WeaponData : ScriptableObject
    {
        public ItemType itemType;   //아이템 타입
        public string itemName;     //아이템 이름
        public Sprite image;        //아이템 이미지
        public float damage;        //총 대미지
        
    }
}
