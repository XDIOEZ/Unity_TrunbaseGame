using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSkill", menuName = "×Ô¶¨Òå/Skill/Skill_Defence")]
public class Skill_Defence : Skill
{
    public override void Use(Character user, Character target)
    {
        if (ChangeActionPoints(user) == false)
        {
            return;
        }
        user.Defense += 1;
    }
}
