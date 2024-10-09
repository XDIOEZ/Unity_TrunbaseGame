using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EntityBaseData
{
    public string name;//实体的名字
    public float _id;// 实体的身份识别码
    public float Max_hp;//最大血量
    public float hp;//当前血量
    public float Max_mp;//最大篮联
    public float mp;//当前篮量
    public float Max_ap;//最大行动点
    public float ap;//当前行动点
    public float Max_defense;//最大防御力
    public float defense;//当前防御力
    public float Max_mr;//最大法术抗性
    public float mr;//当前法术抗性
    public float attack;//实体的攻击力

    public Inventory inventory;//实体的背包
    public Loot_2 loot;//实体的掉落物品
}
