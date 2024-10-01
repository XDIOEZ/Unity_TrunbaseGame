using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "×Ô¶¨Òå/Skill/Skill_Attack")]
public class Skill_Attack : Skill
{
    public  override void Use(Character user, Character target)
    {
        if (ChangeActionPoints(user)==false)
        {
            return;
        }
        target.TakeDamage(user.Attack, target.Defense);
        Debug.Log("Skill_Attack used");
    }
}


