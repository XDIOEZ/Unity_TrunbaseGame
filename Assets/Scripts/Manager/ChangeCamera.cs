using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeCamera : Singleton<ChangeCamera>
{

    void Start()
    {
        EventCenter.Instacne.AddEventListener("ȷ�Ͻ�ɫ�����趨", ToMap);
    }
    public void ToMap()
    {
        Camera.main.transform.position = new Vector3(0, 10, -10);
    }

    public void ToGetSkill()
    {
        Camera.main.transform.position = new Vector3(0, -10, -10);
    }

    public void ToMainPlane()
    {
        Camera.main.transform.position = new Vector3(0, 20, -10);
    }

    public void ChangeCameraTo(float x, float y, float z)
    {
        Camera.main.transform.position = new Vector3(x, y, z);
    }
}
