using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill_2_Attack : Skill_2
{
    public override void Use(Entity user, Entity target)
    {
        Debug.Log("Skill 2 Attack used");
    }
}
