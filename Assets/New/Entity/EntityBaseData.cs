using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class EntityBaseData
{
    public string name;//ʵ�������
    public float _id;// ʵ������ʶ����
    public float Max_hp;//���Ѫ��
    public float hp;//��ǰѪ��
    public float Max_mp;//�������
    public float mp;//��ǰ����
    public float Max_ap;//����ж���
    public float ap;//��ǰ�ж���
    public float Max_defense;//��������
    public float defense;//��ǰ������
    public float Max_mr;//���������
    public float mr;//��ǰ��������
    public float attack;//ʵ��Ĺ�����

    public Inventory inventory;//ʵ��ı���
    public Loot_2 loot;//ʵ��ĵ�����Ʒ
}
