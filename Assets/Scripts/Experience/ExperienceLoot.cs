using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum EmotionType
{
    Negative,
    Positive
}
public class ExperienceLoot : Loot
{
    [Space(10)]
    [Header("ExperienceLoot")]
    [Tooltip("���������� �����")]
    public float ExperienceValue;
    [Tooltip("��� ������")]
    public EmotionType EmotionType;
    
    public override void Take(Collector collector)
    {
        base.Take(collector);
        collector.TakeEmotion(EmotionType, ExperienceValue);
    }
}
  