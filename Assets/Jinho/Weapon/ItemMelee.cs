using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    public class ItemMelee : MonoBehaviour, IAttackItemable, Yeseul.IInteractive, Hojun.IAttackAble
    {
        public WeaponData weaponData;
        public WeaponData WeaponData { get { return weaponData; } }
        public Player Player { get => player; set { player = value; } }
        [SerializeField] Player player = null;
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
                Vector3 tempPos = transform.position;
                if (player.weapon == player.weaponObjSlot[1])   //플레이어가 슬롯의 무기를 들고있을 때,
                {
                    player.weapon = null;
                    player.attackState = ItemType;
                    player.weapon = gameObject;
                    temp.GetComponent<IAttackItemable>().Player = null;
                }
                else
                {                                               //플레이어가 슬롯의 무기를 돌고있지 않을 때,
                    player.weaponObjSlot[1] = null;
                    temp.transform.position = tempPos;
                    temp.GetComponent<IAttackItemable>().Player = null;
                    temp.SetActive(true);
                }
            }
            else
            {           //플레이어의 슬롯이 비었으면
                //player.weaponObjSlot[0].SetActive(false);
                if (player.weapon != null)
                {
                    player.weapon = gameObject;
                    player.attackState = ItemType;
                }
            }
            this.player = player;
            player.weaponObjSlot[1] = gameObject;
        }
        public void Interaction(GameObject interactivePlayer)
        {
            if (interactivePlayer.TryGetComponent(out Player player) && this.player == null)
            {
                SetItem(player);
                col = gameObject.GetComponent<Collider>();
            }
        }
        public void Attack()
        {
            //공격할 때, 일어나는 효과?
            return;
        }
        public GameObject GetAttacker()
        {
            return gameObject;
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out Player player) == this.player) 
            {
                Debug.Log(other.name + "은(는) 주인이다.");
                return;
            }
            if(other.TryGetComponent(out Hojun.IHitAble hit))
            {
                hit.Hit(weaponData.damage, this);
            }
        }
    }
}
