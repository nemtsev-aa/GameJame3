using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PositiveCounter : MonoBehaviour
{
    [Header("Positive Settings")]
    [Tooltip("Текущий уровень позитива")]
    [SerializeField] public float _experiencePositive = 0f;
    [Tooltip("Эффект - получение новой позитивной эмоции")]
    [SerializeField] private ParticleSystem _positiveEffect;
    [Tooltip("Звук - получение позитивной эмоции")]
    [SerializeField] private AudioClip _positiveSound;
    
    [Tooltip("Визуализатор эмоций")]
    [SerializeField] private EmotionValueView _emotionValueView;

    public static PositiveCounter Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);
    }

    private void Update()
    {
        if (_experiencePositive > 100)
        {
            GameStateManager.Instance.SetWin();
        }  
    }

    public void AddPositiveEmotion(float value)
    {
        _experiencePositive += value;
        //_audioSource.PlayOneShot(_positiveSound);
        //_currentParticleSystem = _positiveEffect;

        DisplayExperience();
    }

    public void DisplayExperience()
    {
        _emotionValueView.PositiveValueShow(_experiencePositive);
        //ShowEffect();
    }
}
