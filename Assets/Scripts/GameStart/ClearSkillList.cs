using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearSkillList : MonoBehaviour
{public PlayerSkillInventory playerSkillInventory;
    public void Clear()
    {
        playerSkillInventory.Clear();
    }
}
