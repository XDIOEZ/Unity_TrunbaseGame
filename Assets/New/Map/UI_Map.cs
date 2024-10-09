using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Map : MonoBehaviour
{
    public Map_2 _map; //��ͼ����
    public RectTransform _mapRectTransform; //��ͼ��RectTransform���
    public GameObject UILevelPrefab; //�ؿ�Ԥ����
    void Start()
    {
        SetMapSize(_map.width, _map.height);
        GenerateLevel(_map);
        GenerateLevel(MapMaker.Instance.RandomGenerateMap(1121212));
    }

    void SetMapSize(int width, int height)
    {
        //����ͼ��RectTransform�Ĵ�С����Ϊ��ͼ�Ĵ�С
        _mapRectTransform.sizeDelta = new Vector2(width, height);
    }
    //����Map_2��Level_2���б�����Ԥ������Ϊ�ҽӵ�ͼ _mapRectTransform���Ӷ���
    public void GenerateLevel(Map_2 map)
    {
        for (int i = 0; i < map._LevelAtMap.Count; i++)
        {
           //�Թҽ�Ԥ����Ϊ��������һ���µ�GameObject
            GameObject level = Instantiate(UILevelPrefab, _mapRectTransform);
            //���ùҽ�Ԥ�����λ��
            level.transform.localPosition = new Vector3(map._LevelAtMap[i].LevelPos.x   , map._LevelAtMap[i].LevelPos.y, 0);
            //���ùҽ�Ԥ���������
            level.name = "Level_" + map._LevelAtMap[i].onlyreadylevel.Name;
        }
    }

    
}
