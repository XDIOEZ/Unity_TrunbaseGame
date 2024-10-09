using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameBaseSetting : MonoBehaviour
{
    public Button OpenCharacterCanvas;
    public TMP_InputField text_seed;


    private void Start()
    {
        OpenCharacterCanvas.onClick.AddListener(OnButtonClick);
    }
    void OnButtonClick()
    {
        MapMaker.Instance.SendSeedToGamemanager(text_seed.text);
        EventCenter.Instacne.EventTrigger("确认游戏基础设定");
    }
   


}
