using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public CharacterData characterData;

    public string CharacterName;
    public float Hp;
    public float Attack;
    public float Defense;
    public float CurrentAction;
    public float MaxAction;
    public float Level;

    public List<Skill> Skills;

    protected virtual void Start()
    {
        if (characterData != null)
        {
            InitializeCharacter();
        }
        else
        {
            Debug.LogError("CharacterData is not assigned!");
        }
    }

    private void InitializeCharacter()
    {
        Debug.Log("Initializing Character"+ characterData.characterName);
        CharacterName = characterData.characterName;
        Hp = characterData.maxHp;
        Attack = characterData.attack;
        Defense = characterData.defend;
        MaxAction = characterData.maxAction;
        CurrentAction = MaxAction;
        Level = characterData.level;
        Skills = characterData.baseSkills;
    }

    public virtual float ChangeHp(float value)
    {
        Hp = Mathf.Clamp(Hp + value, 0, characterData.maxHp);
        return Hp;
    }

    public virtual bool TakeDamage(float atk, float def)
    {
        float dmg = atk - def;
        ChangeHp(-dmg);
        return Hp <= 0;
    }
}
