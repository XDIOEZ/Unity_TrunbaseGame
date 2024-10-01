using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "×Ô¶¨Òå/Skill/Skill_SuperContinueAttack")]
public class Skill_SuperContinueAttack : Skill
{
    public override void Use(Character user, Character target)
    {
        if (ChangeActionPoints(user) == false)
        {
            return;
        }
        target.TakeDamage(user.Attack*0.5f, target.Defense);
        target.TakeDamage(user.Attack*0.5f, target.Defense);
        target.TakeDamage(user.Attack*0.5f, target.Defense);
        target.TakeDamage(user.Attack*0.5f, target.Defense);
        target.TakeDamage(user.Attack*0.5f, target.Defense);
        Debug.Log("Skill_Attack used");
    }
}