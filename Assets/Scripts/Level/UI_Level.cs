using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// �ṩ��map�Ĳ��level��UI��ʾ
/// </summary>
public class UI_Level : MonoBehaviour
{
    #region �ɰ��Ա����
    public UI_levelData _data;
    public bool IsHere
    {
        get { return _data.isHere; }
        set
        {
            _data.isHere = value;
        }
    }
    public bool CanMove
    {
        get { return _data.canMove; }
        set
        {
            _data.canMove = value;
        }
    }
    public Level Level
    {
        get { return _data.level; }
        set
        {
            _data.level = value;
        }
    }
    public Vector2Int Position
    {
        get => _data.position;
        set => _data.position = value;
    }
    public Button button; // ��ť���
    public Image UI_Level_image; // ���ڸ�����ʾ��Image���
    #endregion
    #region �ɰ��Ա����
    void Start()
    {
        // ��ȡ��ť���
        if (button != null)
        {
            //Debug.Log("Button found!");
            button.onClick.AddListener(OnButtonClick);
        }
        // ���¸���״̬
        UpdateHighlight();
    }
    void OnButtonClick()// ��Ӧ��ť����¼�
    {
        if (!CanMove)
        {
            return;
        }
        DialogueManager.Instance.ShowDialogue(Level.level_Name + "----" + Level.level_Description);
        DialogueManager.Instance.SetLevelUI(this);
    }
    public void StimulateButtonClick()// �ڶԻ��������,��Ӧ��ť����¼�
    {
        // ��� CanMove ���Բ������ƶ�������Ӧ����¼�
        if (!CanMove)
        {
            return;
        }
        DialogueManager.Instance.SetLevel(Level);
        LootManager.Instance.SetLevelLoot(Level);
        // ���¹ؿ�״̬
        
        // ���¸���״̬
        UpdateHighlight();
    }
    public void UpdateHighlight()//���� IsHere �� CanMove ���Ը��¸�����ʾ
    {
        if (UI_Level_image != null)
        {
            if (IsHere)
            {
                // If 'IsHere' is true, set the color to a light yellow and scale it accordingly
                UI_Level_image.color = Color.green;
                
                UI_Level_image.rectTransform.localScale = Vector3.one * 0.4f ; // Scale to 0.4 times the original size multiplied by 1.3
            }
            else if (CanMove)
            {
                // If 'CanMove' is true, set the color to green
                UI_Level_image.color = new Color(1.0f, 0.92f, 0.016f, 1.0f); // Light yellow color
                UI_Level_image.rectTransform.localScale = Vector3.one * 0.4f * 1.5f; // Scale back to 0.4 times the original size
            }
            else
            {
                // Default case (optional): Reset to original color and scale
                UI_Level_image.color = Color.white; // Or any other default color
                UI_Level_image.rectTransform.localScale = Vector3.one * 0.4f; // Scale back to 0.4 times the original size
            }
        }
    }
    #endregion
    #region �ⲿ���ýӿ�
    public void SetCanMove(bool canMove)  // �����ⲿ���� CanMove ����
    {
        this.CanMove = canMove;
        UpdateHighlight();
    }
    public void SetLevelImage(Sprite image)// ���ùؿ�ͼƬ
    {
        if (UI_Level_image != null)
        {
            UI_Level_image.sprite = image;
            Debug.Log("Level image set!");
            UpdateHighlight();
        }
    }
    #endregion
    #region �°��Ա����
    string levelName;//�ؿ��Ķ�Ӧ���ֵ�Ψһ��ʶ
    public Level level_2;//ֻ��
    int x;//������ͼ�ϵĺ�������
    int y;//������ͼ�ϵ���������
    bool _canMove;//�ؿ��Ƿ���ƶ�
    #endregion
}
