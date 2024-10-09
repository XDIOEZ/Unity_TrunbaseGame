using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState {角色选择 , 地图场景 , 战斗界面 ,搜刮场景 }
public class GameManager_2 : Singleton<GameManager_2>
{
    public UI_Player player;//处于战斗中的玩家引用
    public UI_Enemy enemy;//处于战斗中的敌人引用,引用来自map中的ui_level

    public GameState _gameState   ;//游戏状态 1.角色选择 2.地图界面 3.战斗界面
    public Gamemode _gamemode; //游戏模式

    public UI_Map _map; //保存关卡记录的列表
    public GameSave _GameSave;//引用一个游戏存档

    private void Start()
    {
        
    }
private void Update()
    {
        //按下空格，获取Player_2的Inventory
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.baseData.inventory.skill[0].item.Use(player,player);
        }
    }
}
