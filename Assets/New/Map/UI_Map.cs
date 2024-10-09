using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Map : MonoBehaviour
{
    public Map_2 _map; //地图对象
    public RectTransform _mapRectTransform; //地图的RectTransform组件
    public GameObject UILevelPrefab; //关卡预制体
    void Start()
    {
        SetMapSize(_map.width, _map.height);
        GenerateLevel(_map);
        GenerateLevel(MapMaker.Instance.RandomGenerateMap(1121212));
    }

    void SetMapSize(int width, int height)
    {
        //将地图的RectTransform的大小设置为地图的大小
        _mapRectTransform.sizeDelta = new Vector2(width, height);
    }
    //根据Map_2的Level_2的列表生成预制体作为挂接地图 _mapRectTransform的子对象
    public void GenerateLevel(Map_2 map)
    {
        for (int i = 0; i < map._LevelAtMap.Count; i++)
        {
           //以挂接预制体为基础生成一个新的GameObject
            GameObject level = Instantiate(UILevelPrefab, _mapRectTransform);
            //设置挂接预制体的位置
            level.transform.localPosition = new Vector3(map._LevelAtMap[i].LevelPos.x   , map._LevelAtMap[i].LevelPos.y, 0);
            //设置挂接预制体的名字
            level.name = "Level_" + map._LevelAtMap[i].onlyreadylevel.Name;
        }
    }

    
}
