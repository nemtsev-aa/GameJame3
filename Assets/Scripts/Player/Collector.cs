using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public Camera _camera;

    [Tooltip("������ - �������")]
    public Transform PositiveText;
    [Tooltip("������ - �������")]
    public Transform NefativeText;

    [Tooltip("��������� �����")]
    [SerializeField] private float _distanceToCollect = 2f;
    [Tooltip("���������� ���� ��� ��������� �����")]
    [SerializeField] private LayerMask _layerMask;
    [Tooltip("�������� �����")]
    [SerializeField] private ExperienceManager _experienceManager;

    private void FixedUpdate()
    {
        EmotionCollect();
    }

    private void EmotionCollect()
    {
        // ������ ����������� � �������� ��������������� �������
        Collider[] colliders = Physics.OverlapSphere(transform.position, _distanceToCollect, _layerMask, QueryTriggerInteraction.Ignore);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<ExperienceLoot>() is ExperienceLoot loot)
            {
                if (loot.EmotionType == EmotionType.Positive)
                    loot.Collect(this, PositiveText); // ���� ������ �������������� - ���, ���������� ��������� ������
                else
                    loot.Collect(this, NefativeText); // ���� ������ �������������� - ���, ���������� ��������� ������
            }   
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<EnemyHealth>() is EnemyHealth enemyHealth)
        {
            enemyHealth.TakeDamage(1000);
        }
    }

    public void TakeEmotion(EmotionType emotionType, float value)
    {
        Debug.Log("TakeEmotion: " + value);
        switch (emotionType)
        {
            case EmotionType.Positive:
                _experienceManager.AddPositiveEmotion(value);
                break;
            case EmotionType.Negative:
                _experienceManager.AddNegativeEmotion(value);
                break;
            default:
                break;
        }
    }

#if UNITY_EDITOR
    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.yellow;
        Handles.DrawWireDisc(transform.position, Vector3.up, _distanceToCollect);
    }
#endif
}
