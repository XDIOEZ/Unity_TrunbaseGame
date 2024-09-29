using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;
    public Canvas dialoguePanel; // �Ի����
    public TextMeshProUGUI dialogueText; // �Ի��ı�
    public Button continueButton; // ������ť
    [SerializeField]
    private  Level currentLevel; // ��ǰ�ؿ�
    [SerializeField]
    public  UI_Level ui_Level; // ��UI�ؿ�
    void Awake()
    {
        // ����ģʽ
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
    // ��ʾ�Ի������öԻ�����
    public void ShowDialogue(string content)
    {
        dialoguePanel.enabled = true;
        dialogueText.text = content;
        
    }

    // ���ضԻ���
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
