using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBaseSetting : MonoBehaviour
{   //�����ҽӵİ�ť
    public Button CharacterSelectOverButton;
    private void Start()
    {
        CharacterSelectOverButton.onClick.AddListener(OnButtonClick);
    }
    void OnButtonClick()
    {
        EventCenter.Instacne.EventTrigger("ȷ�Ͻ�ɫ�����趨");
    }
}
