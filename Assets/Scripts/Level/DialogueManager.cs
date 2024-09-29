using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public Canvas dialoguePanel; // 对话面板
    public TextMeshProUGUI dialogueText; // 对话文本
    public Button continueButton; // 继续按钮
    [SerializeField]
    private  Level currentLevel; // 当前关卡
    [SerializeField]
    public  UI_Level ui_Level; // 关UI关卡
    void Awake()
    {
        // 单例模式
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        continueButton.onClick.AddListener(OnContinueButtonClick);
    }

    public void SetLevelUI(UI_Level ui_Level)
    {
        this.ui_Level = ui_Level;
    }
    // 显示对话框并设置对话内容
    public void ShowDialogue(string content)
    {
        dialoguePanel.enabled = true;
        dialogueText.text = content;
        
    }

    // 隐藏对话框
    public void HideDialogue()
    {
        dialoguePanel.enabled = false;
    }
    private void OnContinueButtonClick()
    {
        ui_Level.StimulateButtonClick();
        if (currentLevel != null)
        {
            //BattleSystem.Instance.StartBattle(currentLevel);
            BattleInitiationSystem.Instance.StartBattle();
            HideDialogue();
        }
        
    }
    public void SetLevel(Level level)
    {
        currentLevel = level;
        GameManager.Instance.SetEnemy(level.Level_Battlecharacter[0]);
        Debug.Log("SetLevel in dialogue manager");
    }
}
