using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character
{
    protected override void Start()
    {
        // base.Start(); // ȡ��ֱ�ӵ��û���� Start() ����
        BattleInitiationSystem.Instance.OnBattleStart += InitializeEnemy;
    }

    private void InitializeEnemy()
    {
        base.Start(); // ִ�л���ĳ�ʼ��
        // �������������������Ҫ��ս����ʼʱ��ʼ���Ĳ���
        //Skills
    }
}

