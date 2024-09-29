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

    // ������Ҳ���
    public void SavePlayer(CharacterData playerdata)
    {
        PlayerSave.Save_Player = playerdata;
    }

    // ���� UI_Level ����б�
    public void SaveUILevelComponent_Data(List<UI_levelData> uiLevelComponents)
    {
        PlayerSave.Save_UiLevelData_Components = new List<UI_levelData>(uiLevelComponents);

    }

    // �������λ��
    public void SavePlayerPosition(Vector2Int playerPosition)
    {
        PlayerSave.Save_PlayerPosition = playerPosition;
    }

    // ������Ҽ��ܱ���
    public void SavePlayerSkillsBag(List<Skill> playerSkillsBag)
    {
        PlayerSave.Save_PlayerSkillsBag = new List<Skill>(playerSkillsBag);
    }

/*    // �����ͼ����
    public void SaveMapData(int[] mapData_1D)
    {
        PlayerSave.Save_D_mapData = mapData_1D;
    }*/

    public void Save()
    {
        // ��������һ�� GameManager �� PlayerManager ʵ��
        SavePlayer(GameManager.Instance.player.characterData);
        SavePlayerPosition(Map.Instance.currentPosition.Value);
        /*SaveMapData(Map.Instance.mapData_1D);*/
        SaveUILevelComponent_Data(Map.Instance.uiLevelComponents_Data);
    }
}

