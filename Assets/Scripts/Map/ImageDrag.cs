using UnityEngine;
using UnityEngine.EventSystems;

public class ImageDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IScrollHandler
{
    public RectTransform mapRectTransform; // 地图的RectTransform
    public float dragSpeed = 0.01f; // 拖动速度因子
    public float zoomSpeed = 0.1f; // 缩放速度因子
    public float minZoom = 0.5f; // 最小缩放比例
    public float maxZoom = 3f; // 最大缩放比例

    private Vector3 lastMousePosition; // 上次鼠标位置
    private float currentZoom = 1f; // 当前缩放比例

    public void OnBeginDrag(PointerEventData eventData)
    {
        // 记录开始拖动时的鼠标位置（世界坐标）
        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // 获取当前鼠标位置（世界坐标）
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // 计算鼠标位置的偏移量，并根据拖动速度进行调整
        Vector3 delta = (currentMousePosition - lastMousePosition) * dragSpeed;

        // 更新地图的位置
        mapRectTransform.position += new Vector3(delta.x, delta.y, 0);

        // 更新上次鼠标位置
        lastMousePosition = currentMousePosition;
    }

    public void OnScroll(PointerEventData eventData)
    {
        // 根据滚轮输入调整缩放比例
        float scrollDelta = eventData.scrollDelta.y;
        float newZoom = Mathf.Clamp(currentZoom - scrollDelta * zoomSpeed, minZoom, maxZoom);

        // 获取鼠标在地图局部坐标系中的位置
        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mapRectTransform, Input.mousePosition, Camera.main, out localMousePos);

        // 计算缩放中心的偏移量
        Vector2 offset = localMousePos * (1 - newZoom / currentZoom);

        // 更新缩放比例
        currentZoom = newZoom;
        mapRectTransform.localScale = new Vector3(currentZoom, currentZoom, 1f);

        // 应用缩放中心的偏移量调整地图位置
        mapRectTransform.anchoredPosition += offset;
    }
}
