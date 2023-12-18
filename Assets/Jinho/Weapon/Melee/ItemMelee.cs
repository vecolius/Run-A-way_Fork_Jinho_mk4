using Gayoung;
using Jaeyoung;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Jinho
{
    public class ItemMelee : WeaponMonoBehaviour, IAttackItemable, Yeseul.IInteractive, Hojun.IAttackAble
    {
        public WeaponData weaponData;
        public WeaponData WeaponData { get { return weaponData; } }
        public Player Player 
        { 
            get => player; 
            set 
            { 
                player = value; 
                if(player != null)
                {
                    strategy = new MeleeAttackStrategy(player);
                }
            } 
        }
        [SerializeField] Player player = null;
        public Collider col;
        public AudioClip weaponSound;
        [SerializeField]bool isAttacking = false;

        IAttackStrategy strategy;
        public SoundComponent sound;
        public ItemType ItemType => weaponData.itemType;
        public IAttackStrategy AttackStrategy
        {
            get => strategy;
            set
            {
                strategy = value;
            }
        }
        void OnEnable()
        {
            strategy = new MeleeAttackStrategy(player);
        }
        public void Use()
        {
            strategy.Attack();
        }
        public void Reload()    //근접무기는 재장전 없음
        {
            return;
        }
        public void SetItem(Player player)
        {
            WeaponItem.SetWeapon(player, gameObject, 1, this);
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

        public float GetDamage()
        {
            return WeaponData.damage;
        }

        public void UseEffect()
        {
            //Colldier가 꺼지고 켜짐
            //사운드
            col.enabled = !col.enabled;
            isAttacking = col.enabled;
            if (isAttacking)
            {
                this.sound.ActiveSound();
                GameObject soundObj = PoolingManager.instance.PopObj(PoolingType.SOUND);
                soundObj.transform.position = transform.position;
                AudioSource sound = soundObj.GetComponent<AudioSource>();
                sound.clip = weaponSound;
                soundObj.SetActive(true);
                sound.Play();
            }
        }
    }
}
