using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : MonoBehaviour
{
   //创建单例模式
    public static ChangeCamera instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    public void ChangeCameraToMap()
    {
        Camera.main.transform.position = new Vector3(0, 10, -10);
    }

    public void ChangeCameraToGetSkill()
    {
        Camera.main.transform.position = new Vector3(0, -10, -10);
    }

    public void ChangeCameraToMainPlane()
    {
        Camera.main.transform.position = new Vector3(0, 20, -10);
    }

    public void ChangeCameraTo(float x, float y, float z)
    {
        Camera.main.transform.position = new Vector3(x, y, z);
    }
}
