using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;

[CreateAssetMenu(fileName = "Map_2", menuName = "新建地图")]
public class Map_2 : ScriptableObject
{
    public int _seed;//地图的谁技术种子
    public List<Level_2_Data> _LevelAtMap;//关卡列表
    public int width;      // 地图宽度
    public int height;     //地图高度

  
    
}
[System.Serializable]
public class Level_2_Data
{
    public Level_2 onlyreadylevel;
    public Vector2Int LevelPos;
}
