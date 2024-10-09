using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D.Animation;
using UnityEngine;
public class Entity : MonoBehaviour
{
    public EntityBaseData baseData;//实体的基础数据
    public EntityData entityData;//基础的实体数据
    /// <summary>
    /// 直接修改血量吗,不会计算防御减伤格挡等
    /// </summary>
    /// <param name="value">修改值和正负有关</param>
    /// <returns></returns>
    public virtual float ChangeHp(float value)
    {
        baseData.hp = Mathf.Clamp(baseData.hp + value, 0, baseData.Max_hp);
        return baseData.hp;
    }
     
}
