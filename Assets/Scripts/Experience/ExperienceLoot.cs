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
    [Tooltip("���������� �����")]
    [SerializeField] private int _experienceValue;
    [Tooltip("��� ������")]
    [SerializeField] private EmotionType _emotionType;
    
    public override void Take(Collector collector)
    {
        base.Take(collector);
        collector.TakeEmotion(_emotionType, _experienceValue);
    }
}
  