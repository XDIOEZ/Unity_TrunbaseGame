using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character
{
    protected override void Start()
    {
        // base.Start(); // 取消直接调用基类的 Start() 方法
        BattleInitiationSystem.Instance.OnBattleStart += InitializeEnemy;
    }

    private void InitializeEnemy()
    {
        base.Start(); // 执行基类的初始化
        // 可以在这里进行其他需要在战斗开始时初始化的操作
        //Skills
    }
}

