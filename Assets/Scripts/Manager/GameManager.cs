using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UIElements;

public class GameManager : MonoBehaviour
{
    #region 静态类设置
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
    public GameState gameState; // 游戏回合状态
    public GameObject playerPrefab; // 玩家预制体
    public GameObject enemyPrefab; // 敌人预制体
    public Player player; // 玩家组件
    public Enemy enemy; // 敌人组件
    private int round = 0;
    private UI_Level CurrentUI_level;

    ////////////// UI组件
    public TextMeshProUGUI dialogText; // 对战日志
    public TextMeshProUGUI trucountText; // 显示回合数
    public TextMeshProUGUI trunnameText; // 显示回合名称

    public UI_BalletManagers playerHUD; // 玩家UI管理器
    public UI_BalletManagers enemyHUD; // 敌人UI管理器

    public Canvas VctorCanvas; // 画布

    private void Start()
    {
        enemy = enemyPrefab.GetComponent<Enemy>();
        player = playerPrefab.GetComponent<Player>();
        dialogText.text = "";
        gameState = GameState.Start;
        BattleInitiationSystem.Instance.OnBattleStart += StartGame;
    }

    public void SetEnemy( CharacterData In_enemy)//设置敌人数据
    {
        
        player.characterData = SaveGame.Instance.PlayerSave.Save_Player;
        enemy.characterData = In_enemy; // 设置敌人基础数据
    }
    private void StartGame()//嵌套一个初始化游戏
    {
        StartCoroutine(SetupGame());
    }
    public IEnumerator SetupGame() // 初始化游戏
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
        // 更新玩家和敌人的UI显示
        enemyHUD.UpdateHud(enemy);
        playerHUD.UpdateHud(player);

        // 同步UI显示, 显示回合数和回合名称, 显示玩家和敌人血量
        if (player == null || enemy == null)
        {
            return;
        }

        trunnameText.text = gameState.ToString();
        trucountText.text = player.CurrentAction.ToString();

        // 敌人回合，处理敌人行为树
        if (gameState == GameState.EnemyTurn && enemy.CurrentAction > 0)
        {
            EnemyTurn();
        }

        // 判断游戏是否结束
        if (gameState != GameState.Start)
        {
            IsGameOver();
        }
    }

    #region 玩家点击技能,事件处理
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
    public void SkipPlayerTurn()//玩家点击跳过按钮,事件处理
    {
        if (gameState == GameState.PlayerTurn)
        {
            dialogText.text = "进入敌人回合";
            gameState = GameState.EnemyTurn;
            enemy.CurrentAction = (int)enemy.MaxAction; // 初始化敌人回合数
        }
        else
        {
            Debug.Log("不能跳过玩家回合");
        }
    }
    private void EnemyTurn()//敌人回合事件处理
    {
        // enemy.Skills[i]这里个数组里面有多个技能, 我希望其每一个技能都能发动一次, 所以我需要一个循环
        if (enemy.Skills[round % enemy.Skills.Count] != null)
        {
            enemy.Skills[round % enemy.Skills.Count].Use(enemy, player);
        }

        // 在到达技能表的最后一个技能时我希望可以重新循环技能表, 当然执行完后我希望回到玩家回合
        player.CurrentAction = (int)player.MaxAction;
        gameState = GameState.PlayerTurn;
        round++;
    }

    #region 胜利或失败事件处理
    // 判断游戏是否结束
    private void IsGameOver()
    {
        if (player.Hp <= 0) // 检测玩家是否死亡
        {
            gameState = GameState.Lose;
            dialogText.text = "敌人胜利";
            PlayerLose();
        }
        else if (enemy.Hp <= 0) // 检测敌人是否死亡
        {
            gameState = GameState.Win;
            dialogText.text = "玩家胜利";
            PlayerWin();
        }
    }
    // 处理玩家胜利后的事件
    private void PlayerWin()
    {
        // 更新游戏状态为 Start
        gameState = GameState.Start;
        // 调用 SkillAcquisitionManager 的方法来设置技能
        VctorCanvas.enabled = true;

        CurrentUI_level.IsHere = true;
        CurrentUI_level.CanMove = false;
        Map.Instance.UpdateIsHereStatus(CurrentUI_level.Position);
        CurrentUI_level.UpdateHighlight();
    }
    // 处理战斗失败后的事件
    private void PlayerLose()
    {
        ChangeCamera.instance.ChangeCameraToMap();
        gameState = GameState.Start;
    }
#endregion
}
