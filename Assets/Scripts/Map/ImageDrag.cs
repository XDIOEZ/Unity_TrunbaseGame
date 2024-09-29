using UnityEngine;
using UnityEngine.EventSystems;

public class ImageDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IScrollHandler
{
    public RectTransform mapRectTransform; // ��ͼ��RectTransform
    public float dragSpeed = 0.01f; // �϶��ٶ�����
    public float zoomSpeed = 0.1f; // �����ٶ�����
    public float minZoom = 0.5f; // ��С���ű���
    public float maxZoom = 3f; // ������ű���

    private Vector3 lastMousePosition; // �ϴ����λ��
    private float currentZoom = 1f; // ��ǰ���ű���

    public void OnBeginDrag(PointerEventData eventData)
    {
        // ��¼��ʼ�϶�ʱ�����λ�ã��������꣩
        lastMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    public void OnDrag(PointerEventData eventData)
    {
        // ��ȡ��ǰ���λ�ã��������꣩
        Vector3 currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        // �������λ�õ�ƫ�������������϶��ٶȽ��е���
        Vector3 delta = (currentMousePosition - lastMousePosition) * dragSpeed;

        // ���µ�ͼ��λ��
        mapRectTransform.position += new Vector3(delta.x, delta.y, 0);

        // �����ϴ����λ��
        lastMousePosition = currentMousePosition;
    }

    public void OnScroll(PointerEventData eventData)
    {
        // ���ݹ�������������ű���
        float scrollDelta = eventData.scrollDelta.y;
        float newZoom = Mathf.Clamp(currentZoom - scrollDelta * zoomSpeed, minZoom, maxZoom);

        // ��ȡ����ڵ�ͼ�ֲ�����ϵ�е�λ��
        Vector2 localMousePos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(mapRectTransform, Input.mousePosition, Camera.main, out localMousePos);

        // �����������ĵ�ƫ����
        Vector2 offset = localMousePos * (1 - newZoom / currentZoom);

        // �������ű���
        currentZoom = newZoom;
        mapRectTransform.localScale = new Vector3(currentZoom, currentZoom, 1f);

        // Ӧ���������ĵ�ƫ����������ͼλ��
        mapRectTransform.anchoredPosition += offset;
    }
}
