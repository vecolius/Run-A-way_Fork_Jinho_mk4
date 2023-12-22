using Jinho;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Jaeyoung
{
    public class PlayerUI : MonoBehaviour
    {
        public Player Player
        {
            get
            {
                return player;
            }
            set
            {
                player = value;
                player.initList += UIUpdate;
                UIUpdate();
                gameObject.SetActive(true);
            }
        }
        [SerializeField] Player player;
        public Image hpBar;
        public Image curWeapon;
        public TextMeshProUGUI curAmmo;
        public TextMeshProUGUI totalAmmo;

        public void UIUpdate()
        {
            if (hpBar != null)
            {
                hpBar.transform.localScale = new Vector3(player.state.Hp / player.state.MaxHp, 1, 1);
            }

            if (curWeapon != null)
            {
                curWeapon.sprite = player.currentItemObj.GetComponent<WeaponMonoBehaviour>().WeaponData.image;
            }

            if (curAmmo != null)
            {
                curAmmo.text = player.currentItemObj.GetComponent<WeaponMonoBehaviour>().BulletCount.ToString();
            }

            if (totalAmmo != null)
            {
                totalAmmo.text = player.currentItemObj.GetComponent<WeaponMonoBehaviour>().TotalBullet.ToString();
            }
        }
    }
}