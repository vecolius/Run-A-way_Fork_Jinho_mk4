using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Hojun
{
    public struct CharacterSt
    {

        public float hp;
        public float speed;
        public float isDead;
        public float attackPoint;
        public string characterName;


        public CharacterSt(CharacterSt origin)
        {
            hp = origin.hp;
            speed = origin.speed;
            isDead = origin.isDead;
            attackPoint = origin.attackPoint;
            characterName = origin.characterName;
        }

    }

    [CreateAssetMenu(fileName = "data", menuName = "Scriptable Object/CharacterData", order = int.MaxValue)]
    public class CharacterData : ScriptableObject
    {
        CharacterSt characterSt;

        public CharacterSt GetClone
        {
            get => new CharacterSt(characterSt);
        }

    }
}