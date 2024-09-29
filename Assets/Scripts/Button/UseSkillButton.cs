using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UseSkillButton : MonoBehaviour
{
    [SerializeField]private Skill skillInButton;
    [SerializeField]private TextMeshProUGUI textMesh;

    public void UseSkill()
    {
        GameManager.Instance.UseIndexSkill(skillInButton);
    }
    public void SetSkill(Skill skill)
    {
        skillInButton = skill;
    }
    public void UpdateText()
    {
        this.textMesh.text = skillInButton.skillName;
    }
}
