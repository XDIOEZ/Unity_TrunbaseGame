using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum GameState {��ɫѡ�� , ��ͼ���� , ս������ ,�ѹγ��� }
public class GameManager_2 : Singleton<GameManager_2>
{
    public UI_Player player;//����ս���е��������
    public UI_Enemy enemy;//����ս���еĵ�������,��������map�е�ui_level

    public GameState _gameState   ;//��Ϸ״̬ 1.��ɫѡ�� 2.��ͼ���� 3.ս������
    public Gamemode _gamemode; //��Ϸģʽ

    public UI_Map _map; //����ؿ���¼���б�
    public GameSave _GameSave;//����һ����Ϸ�浵

    private void Start()
    {
        
    }
private void Update()
    {
        //���¿ո񣬻�ȡPlayer_2��Inventory
        if (Input.GetKeyDown(KeyCode.Space))
        {
            player.baseData.inventory.skill[0].item.Use(player,player);
        }
    }
}
