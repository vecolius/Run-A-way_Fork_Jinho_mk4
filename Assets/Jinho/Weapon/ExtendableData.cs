using Jinho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    public interface IExpendable : IUseable 
    {
        public ExtendableData ExtendableData { get; }
    }
    [CreateAssetMenu(fileName = "ExtenableData", menuName = "Scriptable Object/Extenable Data", order = int.MaxValue)]
    public class ExtendableData : ScriptableObject
    {
        public ItemType itemType;   //아이템 타입
        public string itemName;     //아이템 이름
        public Sprite image;        //아이템 이미지
        public float effectValue;   //아이템 효과수치
    }
}
