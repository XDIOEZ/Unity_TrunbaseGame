using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    #region ��̬������
    public static GameManager Instance;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    #endregion
    public enum GameState
    {
        Start, PlayerTurn, EnemyTurn, Win, Lose,
    }
    public GameState gameState; // ��Ϸ�غ�״̬
    public GameObject playerPrefab; // ���Ԥ����
    public GameObject enemyPrefab; // ����Ԥ����
    public Player player; // ������
    public Enemy enemy; // �������
    private int round = 0;
    private UI_Level CurrentUI_level;

    ////////////// UI���
    public TextMeshProUGUI dialogText; // ��ս��־
    public TextMeshProUGUI trucountText; // ��ʾ�غ���
    public TextMeshProUGUI trunnameText; // ��ʾ�غ�����

    public UI_BalletManagers playerHUD; // ���UI������
    public UI_BalletManagers enemyHUD; // ����UI������

    public Canvas VctorCanvas; // ����

    private void Start()
    {
        enemy = enemyPrefab.GetComponent<Enemy>();
        player = playerPrefab.GetComponent<Player>();
        dialogText.text = "";
        gameState = GameState.Start;
        BattleInitiationSystem.Instance.OnBattleStart += StartGame;
    }

    public void SetEnemy( CharacterData In_enemy)//���õ�������
    {
        
        player.characterData = SaveGame.Instance.PlayerSave.Save_Player;
        enemy.characterData = In_enemy; // ���õ��˻�������
    }
    private void StartGame()//Ƕ��һ����ʼ����Ϸ
    {
        StartCoroutine(SetupGame());
    }
    public IEnumerator SetupGame() // ��ʼ����Ϸ
    {
        UseSkillManager.Instance.InstantiationSkillButton(player);
        dialogText.text = enemy.CharacterName + " showTime!!!";
        enemyHUD.UpdateHud(enemy);
        playerHUD.UpdateHud(player);
        CurrentUI_level= DialogueManager.Instance.ui_Level;
        yield return new WaitForSeconds(0f);
        gameState = GameState.PlayerTurn;
    }
    private void Update()
    {
        // ������Һ͵��˵�UI��ʾ
        enemyHUD.UpdateHud(enemy);
        playerHUD.UpdateHud(player);

        // ͬ��UI��ʾ, ��ʾ�غ����ͻغ�����, ��ʾ��Һ͵���Ѫ��
        if (player == null || enemy == null)
        {
            return;
        }

        trunnameText.text = gameState.ToString();
        trucountText.text = player.CurrentAction.ToString();

        // ���˻غϣ����������Ϊ��
        if (gameState == GameState.EnemyTurn && enemy.CurrentAction > 0)
        {
            EnemyTurn();
        }

        // �ж���Ϸ�Ƿ����
        if (gameState != GameState.Start)
        {
            IsGameOver();
        }
    }

    #region ��ҵ������,�¼�����
    public void UseIndexSkill(int index)
    {
        Debug.Log("Use Index Skill");
        if (gameState == GameState.PlayerTurn && player.CurrentAction > 0)
        {
            if (player == null)
            {
                Debug.LogError("Player is null");
                return;
            }

            if (player.Skills == null)
            {
                Debug.LogError("Player's Skills list is null");
                return;
            }

            if (index < 0 || index >= player.Skills.Count)
            {
                Debug.Log("Index is out of bounds");
                return;
            }

            if (player.Skills[index] == null)
            {
                Debug.Log("Skill at index is null");
                return;
            }

            player.Skills[index].Use(player, enemy);
        }
    }
    public void UseIndexSkill(Skill skill)
    {
        skill.Use(player, enemy);
    }
    #endregion
    public void SkipPlayerTurn()//��ҵ��������ť,�¼�����
    {
        if (gameState == GameState.PlayerTurn)
        {
            dialogText.text = "������˻غ�";
            gameState = GameState.EnemyTurn;
            enemy.CurrentAction = (int)enemy.MaxAction; // ��ʼ�����˻غ���
        }
        else
        {
            Debug.Log("����������һغ�");
        }
    }
    private void EnemyTurn()//���˻غ��¼�����
    {
        // enemy.Skills[i]��������������ж������, ��ϣ����ÿһ�����ܶ��ܷ���һ��, ��������Ҫһ��ѭ��
        if (enemy.Skills[round % enemy.Skills.Count] != null)
        {
            enemy.Skills[round % enemy.Skills.Count].Use(enemy, player);
        }

        // �ڵ��＼�ܱ�����һ������ʱ��ϣ����������ѭ�����ܱ�, ��Ȼִ�������ϣ���ص���һغ�
        player.CurrentAction = (int)player.MaxAction;
        gameState = GameState.PlayerTurn;
        round++;
    }

    #region ʤ����ʧ���¼�����
    // �ж���Ϸ�Ƿ����
    private void IsGameOver()
    {
        if (player.Hp <= 0) // �������Ƿ�����
        {
            gameState = GameState.Lose;
            dialogText.text = "����ʤ��";
            PlayerLose();
        }
        else if (enemy.Hp <= 0) // �������Ƿ�����
        {
            gameState = GameState.Win;
            dialogText.text = "���ʤ��";
            PlayerWin();
        }
    }
    // �������ʤ������¼�
    private void PlayerWin()
    {
        // ������Ϸ״̬Ϊ Start
        gameState = GameState.Start;
        // ���� SkillAcquisitionManager �ķ��������ü���
        VctorCanvas.enabled = true;

        CurrentUI_level.IsHere = true;
        CurrentUI_level.CanMove = false;
        Map.Instance.UpdateIsHereStatus(CurrentUI_level.Position);
        CurrentUI_level.UpdateHighlight();
    }
    // ����ս��ʧ�ܺ���¼�
    private void PlayerLose()
    {
        ChangeCamera.instance.ChangeCameraToMap();
        gameState = GameState.Start;
    }
#endregion
}
