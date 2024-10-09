using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLoot", menuName = "�Զ���/�����µ�ս��Ʒ��")]
public class Loot_2 : ScriptableObject // �̳���ScriptableObject������MonoBehaviour
{
    [SerializeField] private int id;
    [SerializeField] private string _name;

    [SerializeField] private List<Items> lootItems; // ʹ�úϲ�������ݽṹ

    // �����ṩһ��ֻ������
    public List<Items> LootItems => lootItems;

    public int ID => id; // ֻ������
    public string Name => _name; // ֻ������
}
/// <summary>
/// �洢��Ʒ�Ļ�����λ
/// </summary>
[System.Serializable]
public class Items
{
    public Item item; // ��Ʒ
    public int amount; // ����

    public Items(Item item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}

