using UnityEngine;

public class ClickObjectLogger : MonoBehaviour
{
    void Update()
    {
        // 检测鼠标左键点击
        if (Input.GetMouseButtonDown(0))
        {
            // 从摄像机发射射线到鼠标点击位置
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // 如果射线击中了某个对象
            if (Physics.Raycast(ray, out hit))
            {
                // 输出被点击的对象的名字
                Debug.Log("Clicked on: " + hit.collider.gameObject.name);
            }
            else
            {
                Debug.Log("No object clicked.");
            }
        }
    }
}
