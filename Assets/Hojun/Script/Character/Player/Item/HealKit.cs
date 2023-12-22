using Jinho;
using System.Collections;
using UnityEngine;

namespace Hojun
{

    public class HealKit : WeaponMonoBehaviour, IUseable, Yeseul.IInteractive
    {
        public AudioClip healSound;
        public GameObject healEffect;
        Animator animator;
        IEnumerator checkMouseCo;


        public ItemType ItemType 
        {
            get => itemType;
            set
            {
                itemType = value;
            }
        }
        ItemType itemType;

        public Player Player 
        { 
            get => player;
            set 
            {
                player = value;
                animator = player.animator;
            }
        }
        public Player player;

        public void Awake()
        {
            itemType = Jinho.ItemType.HealKit;
        }

        // ¾È ¾¸
        public Jinho.IAttackStrategy AttackStrategy 
        { 
            get => null;
            set => throw new System.NotImplementedException();
        }

        public void SetItem(Player player)
        {
            SetWeapon(player, gameObject, 2);
        }


        public void Use()
        {
            StartCoroutine( CheckMouse() );
        }

        IEnumerator CheckMouse()
        {
            while (true) 
            {
                yield return null;

                if (Input.GetKey(KeyCode.Mouse0))
                {
                    player.animator.SetInteger("WeaponType", 6);
                    player.animator.SetBool("HealKit", true);
                    Debug.Log("??");
                    continue;
                }
                player.animator.SetBool("HealKit", false);
                break;
            }

        }


        public void UseEffect()
        {
            SoundEffect(healSound, transform);
            InstantiateEffect(healEffect, player.transform);
            player.Hp += 30;
            Destroy(gameObject);
            player.animator.SetBool("HealKit", false);
            player.WeaponIndex = 1;

        }

        public void Interaction(GameObject interactivePlayer)
        {
            if (interactivePlayer.TryGetComponent(out Player player)) {
                SetItem(player);
            }
        }
    }
}