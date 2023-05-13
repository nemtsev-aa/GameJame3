using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Collector : MonoBehaviour
{
    [Tooltip("��������� �����")]
    [SerializeField] private float _distanceToCollect = 2f;
    [Tooltip("���������� ���� ��� ��������� �����")]
    [SerializeField] private LayerMask _layerMask;
    [Tooltip("�������� �����")]
    [SerializeField] private ExperienceManager _experienceManager;

    private void FixedUpdate()
    {
        // ������ ����������� � �������� ��������������� �������
        Collider[] colliders = Physics.OverlapSphere(transform.position, _distanceToCollect, _layerMask, QueryTriggerInteraction.Ignore);  
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<Loot>() is Loot loot) loot.Collect(this); // ���� ������ �������������� - ���, ���������� ��������� ������
        }
    }

    public void TakeEmotion(EmotionType emotionType, float value)
    {
        switch (emotionType)
        {
            case EmotionType.Positive:
                TakePositiveEmotion(value);
                break;
            case EmotionType.Negative:
                TakeNegativeEmotion(value);
                break;
            default:
                break;
        }
    }

    public void TakePositiveEmotion(float value)
    {
        _experienceManager.AddPositiveEmotion(value);
    }

    public void TakeNegativeEmotion(float value)
    {
        _experienceManager.AddNegativeEmotion(value);
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.up, _distanceToCollect);
    }
#endif
}