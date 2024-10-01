using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{
    Vector2Int playerPosition;//����ڵ�ͼ�ϵ�λ�� 
    int movePoint;//�ƶ��㣬��������ڵ�ͼ���Ƿ���Զ�

    protected override void Start()
    {
        // base.Start(); // ȡ��ֱ�ӵ��û���� Start() ����
        BattleInitiationSystem.Instance.OnBattleStart += InitializePlayer;
    }

    private void InitializePlayer()
    {
        base.Start(); // ִ�л���ĳ�ʼ��
                      // �������������������Ҫ��ս����ʼʱ��ʼ���Ĳ���
        Skills =SaveGame.Instance.PlayerSave.Save_PlayerSkillsBag;//todo ֻͬ��ǰ��������
    }
}
