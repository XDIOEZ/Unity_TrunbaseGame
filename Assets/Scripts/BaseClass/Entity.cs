using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D.Animation;
using UnityEngine;
public class Entity : MonoBehaviour
{
    public float Max_hp;//���Ѫ��
    public float hp;//��ǰѪ��
    public float Max_mp;//�������
    public float mp;//��ǰ����
    public float Max_ap;//����ж���
    public float ap;//��ǰ�ж���
    public float Max_dp;//��������
    public float dp;//��ǰ������
    public float Max_mr;//���������
    public float mr;//��ǰ������������
    /// <summary>
    /// ֱ���޸�Ѫ����,�������������˸񵲵�
    /// </summary>
    /// <param name="value">�޸�ֵ�������й�</param>
    /// <returns></returns>
    public virtual float ChangeHp(float value)
    {
        hp = Mathf.Clamp(hp + value, 0, Max_hp);
        return hp;
    }

}
