using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBaseSetting : MonoBehaviour
{   //监听挂接的按钮
    public Button CharacterSelectOverButton;
    private void Start()
    {
        CharacterSelectOverButton.onClick.AddListener(OnButtonClick);
    }
    void OnButtonClick()
    {
        EventCenter.Instacne.EventTrigger("确认角色基础设定");
    }
}
