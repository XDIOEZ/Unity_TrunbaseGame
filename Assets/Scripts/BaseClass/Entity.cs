using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.U2D.Animation;
using UnityEngine;
public class Entity : MonoBehaviour
{
    public float Max_hp;//最大血量
    public float hp;//当前血量
    public float Max_mp;//最大篮联
    public float mp;//当前篮量
    public float Max_ap;//最大行动点
    public float ap;//当前行动点
    public float Max_dp;//最大防御力
    public float dp;//当前防御力
    public float Max_mr;//最大法术抗性
    public float mr;//当前法术抗性数据
    /// <summary>
    /// 直接修改血量吗,不会计算防御减伤格挡等
    /// </summary>
    /// <param name="value">修改值和正负有关</param>
    /// <returns></returns>
    public virtual float ChangeHp(float value)
    {
        hp = Mathf.Clamp(hp + value, 0, Max_hp);
        return hp;
    }

}
