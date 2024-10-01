using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


/// <summary>
/// 提供给map的插槽level的UI显示
/// </summary>
public class UI_Level : MonoBehaviour
{
    #region 旧版成员变量
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
    public Button button; // 按钮组件
    public Image UI_Level_image; // 用于高亮显示的Image组件
    #endregion
    #region 旧版成员方法
    void Start()
    {
        // 获取按钮组件
        if (button != null)
        {
            //Debug.Log("Button found!");
            button.onClick.AddListener(OnButtonClick);
        }
        // 更新高亮状态
        UpdateHighlight();
    }
    void OnButtonClick()// 响应按钮点击事件
    {
        if (!CanMove)
        {
            return;
        }
        DialogueManager.Instance.ShowDialogue(Level.level_Name + "----" + Level.level_Description);
        DialogueManager.Instance.SetLevelUI(this);
    }
    public void StimulateButtonClick()// 在对话框处理完毕,响应按钮点击事件
    {
        // 如果 CanMove 属性不允许移动，不响应点击事件
        if (!CanMove)
        {
            return;
        }
        DialogueManager.Instance.SetLevel(Level);
        LootManager.Instance.SetLevelLoot(Level);
        // 更新关卡状态
        
        // 更新高亮状态
        UpdateHighlight();
    }
    public void UpdateHighlight()//根据 IsHere 和 CanMove 属性更新高亮显示
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
    #region 外部调用接口
    public void SetCanMove(bool canMove)  // 允许外部设置 CanMove 属性
    {
        this.CanMove = canMove;
        UpdateHighlight();
    }
    public void SetLevelImage(Sprite image)// 设置关卡图片
    {
        if (UI_Level_image != null)
        {
            UI_Level_image.sprite = image;
            Debug.Log("Level image set!");
            UpdateHighlight();
        }
    }
    #endregion
    #region 新版成员变量
    string levelName;//关卡的对应名字的唯一标识
    public Level level_2;//只读
    int x;//所处地图上的横轴坐标
    int y;//所处地图上的纵轴坐标
    bool _canMove;//地块是否可移动
    #endregion
}
