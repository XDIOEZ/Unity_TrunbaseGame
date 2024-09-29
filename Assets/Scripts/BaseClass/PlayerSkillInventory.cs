using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New PlayerSkillInventory", menuName = "×Ô¶¨Òå/Create New PlayerSave")]
public class PlayerSkillInventory : ScriptableObject
{
    public CharacterData Save_Player;
    public List<UI_levelData> Save_UiLevelData_Components;
    public Vector2Int Save_PlayerPosition;
    public List<Skill> Save_PlayerSkillsBag;
    public int Save_Seed;


    public void Clear()
    {
        Save_PlayerSkillsBag.Clear();
    }
    public bool AddSkill(Skill skill)
    {
        if (Save_PlayerSkillsBag.Contains(skill))
        {
            return false;
        }
        else
        {
            Save_PlayerSkillsBag.Add(skill);
            return true;
        }
    }
    public bool RemoveSkill(Skill skill)
    {
        if (Save_PlayerSkillsBag.Contains(skill))
        {
            Save_PlayerSkillsBag.Remove(skill);
            return true;
        }
        else
        {
            return false;
        }
    }
    public bool HasSkill(Skill skill)
    {
        return Save_PlayerSkillsBag.Contains(skill);
    }

    
}
