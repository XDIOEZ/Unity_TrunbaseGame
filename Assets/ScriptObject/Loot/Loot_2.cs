using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLoot", menuName = "�Զ���/�����µ�ս��Ʒ��")]
public class Loot_2 : ScriptableObject // �̳���ScriptableObject������MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private string _name;

    [SerializeField] private List<LootItem> lootItems; // ʹ�úϲ�������ݽṹ

    // �����ṩһ��ֻ������
    public List<LootItem> LootItems => lootItems;

    public int ID => id; // ֻ������
    public string Name => _name; // ֻ������
}

[System.Serializable]
public class LootItem
{
    public Item item; // ��Ʒ
    public int amount; // ����

    public LootItem(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}

