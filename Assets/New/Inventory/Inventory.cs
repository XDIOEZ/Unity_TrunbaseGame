using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewLevel", menuName = "�����µ�����")]
/// <summary>
/// �洢��ɫ�ĵ��ߡ����ܡ��츳�����
/// </summary>
public class Inventory : ScriptableObject
{
    public List<Items> skill;//�����б�
    public List<Items> prop;//�����б�
    public List<Items> talent;//�����б�

    public int gold; //������� 
    
}


