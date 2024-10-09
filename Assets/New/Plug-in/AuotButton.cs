using UnityEngine;
using UnityEngine.UI;  // ȷ����������UI�����ռ�

public class AuotButton : MonoBehaviour
{
    private Canvas parentCanvas;
    private Button button;

    void Start()
    {
        // ��ȡ�������Canvas���
        parentCanvas = GetComponentInParent<Canvas>();

        // ��ȡ�����Button���
        button = GetComponent<Button>();

        if (parentCanvas != null && button != null)
        {
            // ������ť�ĵ���¼�
            button.onClick.AddListener(ToggleCanvasVisibility);
        }
        else
        {
            Debug.LogWarning("Canvas �� Button δ�ҵ�����ȷ�������ڸ�����͵�ǰ�����ϴ��ڡ�");
        }
    }

    // �л�Canvas����
    private void ToggleCanvasVisibility()
    {
        if (parentCanvas != null)
        {
            parentCanvas.enabled = !parentCanvas.enabled;  // �л�Canvas��enabled����
        }
    }
}
