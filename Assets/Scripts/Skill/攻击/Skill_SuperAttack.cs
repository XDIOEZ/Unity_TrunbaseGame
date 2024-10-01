using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "×Ô¶¨Òå/Skill/Skill_SuperAttackAttack")]
public class Skill_SuperAttack : Skill
{
    public override void Use(Character user, Character target)
    {
        if (ChangeActionPoints(user) == false)
        {
            return;
        }
        target.TakeDamage(user.Attack*1.8f, target.Defense);
        Debug.Log("Skill_Attack used");
    }
}