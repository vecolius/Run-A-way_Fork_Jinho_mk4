using Hojun;
using Jinho;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiHpBar : UiView
{
    public float hp;
    public float maxHp;
    public Image hpBar;
    public CharacterUiModel model;

    public PlayerData Data 
    {
        get
        {
            return playerData;
        }
        set
        {
            playerData = value;
            maxHp = playerData.MaxHp;
            hp = playerData.Hp;
            ViewData();

            model.AddEvent(ViewData);
        }
    }
    PlayerData playerData;

    public override void ViewData()
    {
        hpBar.fillAmount = hp / maxHp;
    }
}
