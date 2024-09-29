using System;
using UnityEngine;

public class StartGame : MonoBehaviour
{
    // ����һ����Ϸ��ʼ��ί��
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
