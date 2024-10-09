using UnityEngine;
using UnityEngine.UI;  // 确保你引用了UI命名空间

public class AuotButton : MonoBehaviour
{
    private Canvas parentCanvas;
    private Button button;

    void Start()
    {
        // 获取父对象的Canvas组件
        parentCanvas = GetComponentInParent<Canvas>();

        // 获取自身的Button组件
        button = GetComponent<Button>();

        if (parentCanvas != null && button != null)
        {
            // 监听按钮的点击事件
            button.onClick.AddListener(ToggleCanvasVisibility);
        }
        else
        {
            Debug.LogWarning("Canvas 或 Button 未找到，请确保它们在父对象和当前对象上存在。");
        }
    }

    // 切换Canvas开关
    private void ToggleCanvasVisibility()
    {
        if (parentCanvas != null)
        {
            parentCanvas.enabled = !parentCanvas.enabled;  // 切换Canvas的enabled属性
        }
    }
}
