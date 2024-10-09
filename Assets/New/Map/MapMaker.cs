using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : Singleton<MapMaker>
{
    int Now_seed;
    
    void Start()
    {
        EventCenter.Instacne.AddEventListener("ȷ����Ϸ�����趨", LodgingMapBySeed);
    }

    private void LodgingMapBySeed()
    {
        Debug.Log(Now_seed);
    }
    //�Զ����Ⱥ͸߶�,�Զ���ؿ�����
    public  Map_2 RandomGenerateMap(int seed, int customWidth=0, int customHeight = 0, int levelCount = 10)
    {
        //�������һ����ͼ
        //ֱ�Ӵ�����ͼ����SO,Ȼ����Map_2�ű������õ�ͼ������

        // ʹ�����ӳ�ʼ�������������
        Random.InitState(seed);
        // ʹ�����ӳ�ʼ�������������
        Map_2 map = ScriptableObject.CreateInstance<Map_2>();
        // �趨��ͼ����
        map._seed = seed;
        // �ж�����Ƿ��ṩ�Զ����Ⱥ͸߶�
        if (customWidth <= 0 || customHeight <= 0)
        {
            // ������û���Զ����Ȼ�߶ȣ�ʹ��������ɵ�ֵ
            map.width = Random.Range(10, 50);  // �����ȷ�Χ 10 �� 50
            map.height = Random.Range(10, 50); // ����߶ȷ�Χ 10 �� 50
        }
        else
        {
            // ����Զ����˿�Ⱥ͸߶ȣ�ֱ��ʹ����ҵ�ֵ
            map.width = customWidth;
            map.height = customHeight;
        }
        // ��ʼ���ؿ��б�
        map._LevelAtMap = new List<Level_2_Data>();
        // ������ɹؿ�����
        if(levelCount <= 0)
        {
            levelCount = Random.Range(1, customWidth * customHeight); // �������
        }

        for (int i = 0; i < levelCount; i++)
        {
            Level_2_Data levelData = new Level_2_Data();

            // ������ɹؿ���λ��
            levelData.LevelPos = new Vector2Int(
                Random.Range(0, map.width),
                Random.Range(0, map.height)
            );

            // �����ͨ���Զ����߼������� `onlyreadylevel`���������ѡȡһ��Ԥ����Ĺؿ�
            levelData.onlyreadylevel = GetRandomLevel(); // �Զ���Ĺؿ���ȡ�߼�

            // ���ؿ����ݼ����ͼ�Ĺؿ��б�
            map._LevelAtMap.Add(levelData);
        }

        return map;
    }

    public Level_2 GetRandomLevel()
    {
        // ������һЩ�ؿ�����������ѡ��
        // ���ﷵ��һ������� `Level_2` ��������Ҫ���������Ŀ����ʵ��
        return new Level_2();
    }

    /// <summary>
    /// �������seed���͵�GameManager_2��
    /// </summary>
    /// <param name="inputSeed"></param>
    public void SendSeedToGamemanager(string inputSeed)
    {
        int seed;

        // �������Ϊ�ջ�ȫ�ǿո�������һ���������Ϊseed
        if (string.IsNullOrWhiteSpace(inputSeed))
        {
            seed = Random.Range(1, 1000000);
        }
        // ��������seed�����֣���ֱ��ת��Ϊ����
        else if (int.TryParse(inputSeed, out seed))
        {
            if (seed == 0)
            {
                // ��������seedΪ0������һ���������Ϊseed
                seed = Random.Range(1, 1000000);
            }
        }
        else
        {
            // ��������seed�������֣����������ַ�������һ���������
            seed = inputSeed.GetHashCode();
        }
        seed = this.Now_seed;
    }
}
