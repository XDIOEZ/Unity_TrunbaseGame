using System;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    // 定义一个游戏开始的委托
    public delegate void GameStartAction();
    public static event GameStartAction OnGameStart;

    public PlayerSkillInventory playerSkillInventory;

    public void StartAGame()
    {
        playerSkillInventory.Clear();
        OnGameStart?.Invoke();
       SaveGame.Instance.PlayerSave.Save_Seed = GameSettings.instance.seed;
    }
}
