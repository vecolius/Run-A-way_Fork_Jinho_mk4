using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    public class HealKit : IExpendable
    {
        ExtendableData extendableData;
        public ExtendableData ExtendableData { get => extendableData; set {  extendableData = value; } }

        public ItemType ItemType => throw new System.NotImplementedException();

        public Player Player { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

        public void Reload()
        {
            throw new System.NotImplementedException();
        }

        public void SetItem(Player player)
        {
            throw new System.NotImplementedException();
        }

        public void Use() 
        {
            
        }
    }

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
