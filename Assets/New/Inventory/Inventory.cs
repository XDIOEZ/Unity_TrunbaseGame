using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "创建新的容器")]
/// <summary>
/// 存储角色的道具、技能、天赋，金币
/// </summary>
public class Inventory : ScriptableObject
{
    public List<Items> skill;//道具列表
    public List<Items> prop;//道具列表
    public List<Items> talent;//道具列表

    public int gold; //金币数量 
    
}


