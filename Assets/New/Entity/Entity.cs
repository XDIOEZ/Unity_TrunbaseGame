using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D.Animation;
using UnityEngine;
public class Entity : MonoBehaviour
{
    public EntityBaseData baseData;//ʵ��Ļ�������
    public EntityData entityData;//������ʵ������
    /// <summary>
    /// ֱ���޸�Ѫ����,�������������˸񵲵�
    /// </summary>
    /// <param name="value">�޸�ֵ�������й�</param>
    /// <returns></returns>
    public virtual float ChangeHp(float value)
    {
        baseData.hp = Mathf.Clamp(baseData.hp + value, 0, baseData.Max_hp);
        return baseData.hp;
    }
     
}
