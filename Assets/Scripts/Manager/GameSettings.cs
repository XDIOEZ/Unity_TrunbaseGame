using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSettings : MonoBehaviour
{
    public static GameSettings instance;
    public  int seed; // ���ڱ������ɵ����������

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            StartGame.OnGameStart += GenerateSeed;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void GenerateSeed()
    {
        seed = Random.Range(0, int.MaxValue); // ����һ���Ǹ�������Ϊ����
        Random.InitState(seed); // ʹ�����ɵ����ӳ�ʼ�������������
        Debug.Log("Random Now_seed generated: " + seed); // ������ɵ������Ա����
    }
}
