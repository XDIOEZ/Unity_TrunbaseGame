using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings instance;
    public  int seed; // 用于保存生成的随机数种子

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            StartGame.OnGameStart += GenerateSeed;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void GenerateSeed()
    {
        seed = Random.Range(0, int.MaxValue); // 生成一个非负整数作为种子
        Random.InitState(seed); // 使用生成的种子初始化随机数生成器
        Debug.Log("Random Now_seed generated: " + seed); // 输出生成的种子以便调试
    }
}
