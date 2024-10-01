using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    Vector2Int playerPosition;//玩家在地图上的位置 
    int movePoint;//移动点，决定玩家在地图上是否可以动

    protected override void Start()
    {
        // base.Start(); // 取消直接调用基类的 Start() 方法
        BattleInitiationSystem.Instance.OnBattleStart += InitializePlayer;
    }

    private void InitializePlayer()
    {
        base.Start(); // 执行基类的初始化
                      // 可以在这里进行其他需要在战斗开始时初始化的操作
        Skills =SaveGame.Instance.PlayerSave.Save_PlayerSkillsBag;//todo 只同步前几个技能
    }
}
