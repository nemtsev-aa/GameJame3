using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Collector : MonoBehaviour
{
    public Camera _camera;

    [Tooltip("Запись - позитив")]
    public Transform PositiveText;
    [Tooltip("Запись - негатив")]
    public Transform NefativeText;

    [Tooltip("Дистанция сбора")]
    [SerializeField] private float _distanceToCollect = 2f;
    [Tooltip("Физический слой для обработки сбора")]
    [SerializeField] private LayerMask _layerMask;
    [Tooltip("Менеджер опыта")]
    [SerializeField] private ExperienceManager _experienceManager;

    private void FixedUpdate()
    {
        EmotionCollect();
    }

    private void EmotionCollect()
    {
        // Массив коллайдеров с которыми взаимодействует сборщик
        Collider[] colliders = Physics.OverlapSphere(transform.position, _distanceToCollect, _layerMask, QueryTriggerInteraction.Ignore);
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].GetComponent<ExperienceLoot>() is ExperienceLoot loot)
            {
                if (loot.EmotionType == EmotionType.Positive)
                {
                    _experienceManager.AddPositiveEmotion(loot.ExperienceValue);
                    loot.Collect(this, PositiveText); // Если объект взаимодействия - лут, активируем процедуру сборки
                }  
                else
                {
                    _experienceManager.AddNegativeEmotion(loot.ExperienceValue);
                    loot.Collect(this, NefativeText); // Если объект взаимодействия - лут, активируем процедуру сборки
                }
                
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
