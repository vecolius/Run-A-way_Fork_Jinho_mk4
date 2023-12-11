using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    public class ItemMelee : MonoBehaviour, IAttackItemable
    {
        public WeaponData weaponData;
        public WeaponData WeaponData { get { return weaponData; } }
        public Player Player { get => player; set { player = value; } }
        Player player = null;
        public Collider col;
        public ItemType ItemType => weaponData.itemType;
        public void Use()
        {
            //Colldier가 꺼지고 켜짐
            //사운드
            col.enabled = !col.enabled;
        }
        public void Reload()    //근접무기는 재장전 없음
        {
            return;
        }
        public void SetItem(Player player)
        {
            if (player.weaponObjSlot[1] != null)
            {
                GameObject temp = player.weaponObjSlot[1];
                temp.transform.position = transform.position;
                temp.GetComponent<IAttackItemable>().Player = null;
                player.weaponObjSlot[1] = null;
                temp.SetActive(true);
            }
            this.player = player;
            player.weaponObjSlot[1] = gameObject;
            player.weaponObjSlot[1].SetActive(false);
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player) && this.player == null)
            {
                SetItem(player);
                col = gameObject.GetComponent<Collider>();
            }
            //if(other.TryGetComponent(out IHitable hit))
        }
    }
}
