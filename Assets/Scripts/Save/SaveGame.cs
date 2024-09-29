using System.Collections.Generic;
using UnityEngine;

public class SaveGame : MonoBehaviour
{
    public static SaveGame Instance;

    public PlayerSkillInventory PlayerSave;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // 保存玩家参数
    public void SavePlayer(CharacterData playerdata)
    {
        PlayerSave.Save_Player = playerdata;
    }

    // 保存 UI_Level 组件列表
    public void SaveUILevelComponent_Data(List<UI_levelData> uiLevelComponents)
    {
        PlayerSave.Save_UiLevelData_Components = new List<UI_levelData>(uiLevelComponents);

    }

    // 保存玩家位置
    public void SavePlayerPosition(Vector2Int playerPosition)
    {
        PlayerSave.Save_PlayerPosition = playerPosition;
    }

    // 保存玩家技能背包
    public void SavePlayerSkillsBag(List<Skill> playerSkillsBag)
    {
        PlayerSave.Save_PlayerSkillsBag = new List<Skill>(playerSkillsBag);
    }

/*    // 保存地图数据
    public void SaveMapData(int[] mapData_1D)
    {
        PlayerSave.Save_D_mapData = mapData_1D;
    }*/

    public void Save()
    {
        // 假设你有一个 GameManager 或 PlayerManager 实例
        SavePlayer(GameManager.Instance.player.characterData);
        SavePlayerPosition(Map.Instance.currentPosition.Value);
        /*SaveMapData(Map.Instance.mapData_1D);*/
        SaveUILevelComponent_Data(Map.Instance.uiLevelComponents_Data);
    }
}

