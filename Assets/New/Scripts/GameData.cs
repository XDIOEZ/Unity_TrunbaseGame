using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static GameManager;

public enum Gamemode//����Gamemode
{
    ��,
    ��ͨ,
    ����
}
public class GameData : MonoBehaviour
{
    public Map _map; //����ؿ���¼���б�
    public Gamemode _gamemode; //��Ϸģʽ
    public GameSave _GameSave;//����һ����Ϸ�浵
   public int seed;//������ɵ�ͼ����Ҫ������
}
