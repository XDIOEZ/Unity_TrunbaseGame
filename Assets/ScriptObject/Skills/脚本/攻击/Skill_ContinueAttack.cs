using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewCharacter", menuName = "�Զ���/Skill/Skill_Skill_ContinuousAttack")]
public class Skill_ContinuousAttack : Skill
{
    public override void Use(Character user, Character target)
    {
        if (ChangeActionPoints(user) == false)
        {
            return;
        }
        target.TakeDamage(user.Attack*0.6f, target.Defense);
        target.TakeDamage(user.Attack*0.6f, target.Defense);
        target.TakeDamage(user.Attack*0.6f, target.Defense);
        Debug.Log("Skill_Attack used");
    }
}