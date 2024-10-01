using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;


public class GameData : MonoBehaviour
{
    enum Gamemode//创建Gamemode
    {
        简单,
        普通,
        困难
    }
    Player _player;//玩家角色
    Map _map; //保存关卡记录的列表
    Gamemode _gamemode; //游戏模式
    GameSave _GameSave;//引用一个游戏存档
    int seed;//随机生成地图所需要的种子
}
