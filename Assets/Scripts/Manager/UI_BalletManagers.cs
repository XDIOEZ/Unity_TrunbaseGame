using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BalletManagers : MonoBehaviour
{
   // public static UIManager Instance;

    public TextMeshProUGUI nameText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI HpText;
    public TextMeshProUGUI DefenseText;
    public TextMeshProUGUI AttackText;
    public Slider hpSlider;


    
    // Start is called before the first frame update
    public void UpdateHud(Character character)
    {
        if (character == null)
        {
            //Debug.Log("Character is null");
            return;
        }
        nameText.text = character.CharacterName;
        LevelText.text = "Level: " + character.Level.ToString();
        HpText.text = "HP: " + character.Hp.ToString();
        DefenseText.text = "Defense: " + character.Defense.ToString();
        AttackText.text = "Attack: " + character.Attack.ToString();

        hpSlider.maxValue = character.characterData.maxHp;
        hpSlider.value = character.Hp;
    }
}

