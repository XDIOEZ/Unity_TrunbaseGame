using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Player : Entity
{
    // Start is called before the first frame update
    void Start()
    {
        SetUI_PlayerData(base.entityData.EntityBaseData);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    /// <summary>
    /// 将传入的玩家基础数值赋值到战斗UI的Player上面
    /// </summary>
    /// <param name="entityBaseData"></param>
    void SetUI_PlayerData(EntityBaseData entityBaseData )
    {
        base.baseData = entityBaseData;
    }
}
