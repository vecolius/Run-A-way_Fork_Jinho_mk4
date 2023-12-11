using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "data", menuName = "Scriptable Object/CharacterData", order = int.MaxValue)]
public class CharacterData : ScriptableObject
{
    public float hp;
    public float speed;
    public bool isDead;
    public float attackPoint;
    public string characterName;

}
