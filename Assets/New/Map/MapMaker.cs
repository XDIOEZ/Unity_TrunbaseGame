using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapMaker : Singleton<MapMaker>
{
    int Now_seed;
    
    void Start()
    {
        EventCenter.Instacne.AddEventListener("确认游戏基础设定", LodgingMapBySeed);
    }

    private void LodgingMapBySeed()
    {
        Debug.Log(Now_seed);
    }
    //自定义宽度和高度,自定义关卡数量
    public  Map_2 RandomGenerateMap(int seed, int customWidth=0, int customHeight = 0, int levelCount = 10)
    {
        //随机生成一个地图
        //直接创建地图对象SO,然后在Map_2脚本中设置地图的属性

        // 使用种子初始化随机数生成器
        Random.InitState(seed);
        // 使用种子初始化随机数生成器
        Map_2 map = ScriptableObject.CreateInstance<Map_2>();
        // 设定地图属性
        map._seed = seed;
        // 判断玩家是否提供自定义宽度和高度
        if (customWidth <= 0 || customHeight <= 0)
        {
            // 如果玩家没有自定义宽度或高度，使用随机生成的值
            map.width = Random.Range(10, 50);  // 随机宽度范围 10 到 50
            map.height = Random.Range(10, 50); // 随机高度范围 10 到 50
        }
        else
        {
            // 玩家自定义了宽度和高度，直接使用玩家的值
            map.width = customWidth;
            map.height = customHeight;
        }
        // 初始化关卡列表
        map._LevelAtMap = new List<Level_2_Data>();
        // 随机生成关卡数量
        if(levelCount <= 0)
        {
            levelCount = Random.Range(1, customWidth * customHeight); // 随机生成
        }

        for (int i = 0; i < levelCount; i++)
        {
            Level_2_Data levelData = new Level_2_Data();

            // 随机生成关卡的位置
            levelData.LevelPos = new Vector2Int(
                Random.Range(0, map.width),
                Random.Range(0, map.height)
            );

            // 你可以通过自定义逻辑来设置 `onlyreadylevel`，比如随机选取一个预定义的关卡
            levelData.onlyreadylevel = GetRandomLevel(); // 自定义的关卡获取逻辑

            // 将关卡数据加入地图的关卡列表
            map._LevelAtMap.Add(levelData);
        }

        return map;
    }

    public Level_2 GetRandomLevel()
    {
        // 假设有一些关卡对象可以随机选择
        // 这里返回一个随机的 `Level_2` 对象，你需要根据你的项目具体实现
        return new Level_2();
    }

    /// <summary>
    /// 将输入的seed发送到GameManager_2中
    /// </summary>
    /// <param name="inputSeed"></param>
    public void SendSeedToGamemanager(string inputSeed)
    {
        int seed;

        // 如果输入为空或全是空格，则生成一个随机数作为seed
        if (string.IsNullOrWhiteSpace(inputSeed))
        {
            seed = Random.Range(1, 1000000);
        }
        // 如果输入的seed是数字，则直接转换为整数
        else if (int.TryParse(inputSeed, out seed))
        {
            if (seed == 0)
            {
                // 如果输入的seed为0，生成一个随机数作为seed
                seed = Random.Range(1, 1000000);
            }
        }
        else
        {
            // 如果输入的seed不是数字，根据输入字符串生成一个随机种子
            seed = inputSeed.GetHashCode();
        }
        seed = this.Now_seed;
    }
}
