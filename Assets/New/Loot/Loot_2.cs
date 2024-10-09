using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLoot", menuName = "自定义/创建新的战利品表")]
public class Loot_2 : ScriptableObject // 继承自ScriptableObject而不是MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private string _name;

    [SerializeField] private List<Items> lootItems; // 使用合并后的数据结构

    // 可以提供一个只读属性
    public List<Items> LootItems => lootItems;

    public int ID => id; // 只读属性
    public string Name => _name; // 只读属性
}
/// <summary>
/// 存储物品的基本单位
/// </summary>
[System.Serializable]
public class Items
{
    public Item item; // 物品
    public int amount; // 数量

    public Items(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}

