using Jinho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    [CreateAssetMenu(fileName = "WeaponData", menuName = "Scriptable Object/Weapon Data", order = int.MaxValue)]
    public class WeaponData : ScriptableObject
    {
        public ItemType itemType;   //아이템 타입
        public string itemName;     //아이템 이름
        public Sprite image;        //아이템 이미지
        public float damage;        //총 대미지
        public int bullet;
        public int maxBullet;

        public WeaponData Clone 
        {
            get
            {
                return Instantiate(this);
            }
        }
    }
}
