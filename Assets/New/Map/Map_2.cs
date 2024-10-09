using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[CreateAssetMenu(fileName = "Map_2", menuName = "�½���ͼ")]
public class Map_2 : ScriptableObject
{
    public int _seed;//��ͼ��˭��������
    public List<Level_2_Data> _LevelAtMap;//�ؿ��б�
    public int width;      // ��ͼ���
    public int height;     //��ͼ�߶�

  
    
}
[System.Serializable]
public class Level_2_Data
{
    public Level_2 onlyreadylevel;
    public Vector2Int LevelPos;
}
