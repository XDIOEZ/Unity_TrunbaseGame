using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;


public class GameData : MonoBehaviour
{
    enum Gamemode//����Gamemode
    {
        ��,
        ��ͨ,
        ����
    }
    Player _player;//��ҽ�ɫ
    Map _map; //����ؿ���¼���б�
    Gamemode _gamemode; //��Ϸģʽ
    GameSave _GameSave;//����һ����Ϸ�浵
    int seed;//������ɵ�ͼ����Ҫ������
}
