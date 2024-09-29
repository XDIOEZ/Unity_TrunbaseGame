using UnityEngine;

public class ClickObjectLogger : MonoBehaviour
{
    void Update()
    {
        // ������������
        if (Input.GetMouseButtonDown(0))
        {
            // ��������������ߵ������λ��
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // ������߻�����ĳ������
            if (Physics.Raycast(ray, out hit))
            {
                // ���������Ķ��������
                Debug.Log("Clicked on: " + hit.collider.gameObject.name);
            }
            else
            {
                Debug.Log("No object clicked.");
            }
        }
    }
}
