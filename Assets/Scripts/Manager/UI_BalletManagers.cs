using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_BalletManagers : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI HpText;
    public TextMeshProUGUI DefenseText;
    public TextMeshProUGUI AttackText;
    public Slider hpSlider;
    public Entity entity;

    void Start()
    {
       
    }

    void Update()
    {
        UpdateHud(entity);
    }
    // Start is called before the first frame update
    public void UpdateHud(Entity entity)
    {
       
        nameText.text = entity.baseData.name;
        HpText.text = "HP: " + entity.baseData.hp.ToString();
        DefenseText.text = "Defense: " + entity.baseData.defense.ToString();
        AttackText.text = "Attack: " + entity.baseData.attack.ToString();

        hpSlider.maxValue = entity.baseData.Max_hp;
        hpSlider.value = entity.baseData.hp;
    }
}

