using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContinueButton : MonoBehaviour
{
    public PlayerSkillInventory Save;

    public void ContinueGame()
    {
        Map.Instance.ContinueGame(Save.Save_PlayerPosition,Save.Save_UiLevelData_Components);
        Camera.main.transform.position = new Vector3(0, 10, -10);
    }
}
