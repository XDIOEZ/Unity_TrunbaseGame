using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public enum Gamemode//创建Gamemode
{
    简单,
    普通,
    困难
}
public class GameData : MonoBehaviour
{
    public Map _map; //保存关卡记录的列表
    public Gamemode _gamemode; //游戏模式
    public GameSave _GameSave;//引用一个游戏存档
   public int seed;//随机生成地图所需要的种子
}
