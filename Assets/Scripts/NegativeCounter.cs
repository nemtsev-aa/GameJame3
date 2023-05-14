using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NegativeCounter : MonoBehaviour
{
    [Header("Negative Settings")]
    [Tooltip("Текущий уровень негатива")]
    [SerializeField] public float _experienceNegative = 0f;
    [Tooltip("Эффект - получение новой негативной эмоции")]
    [SerializeField] private ParticleSystem _negativeEffect;
    [Tooltip("Звук - получение негативной эмоции")]
    [SerializeField] private AudioClip _negativeSound;

    [Tooltip("Визуализатор эмоций")]
    [SerializeField] private EmotionValueView _emotionValueView;

    public static NegativeCounter Instance;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else if (Instance == this)
            Destroy(gameObject);
    }

    private void Update()
    {
        if (_experienceNegative > 100)
        {
            GameStateManager.Instance.SetLose();
        }
    }

    public void AddNegativeEmotion(float value)
    {
        _experienceNegative += value;
        //_audioSource.PlayOneShot(_negativeSound);
        //_currentParticleSystem = _negativeEffect;

        DisplayExperience();
    }

    public void DisplayExperience()
    {
        _emotionValueView.NegativeValueShow(_experienceNegative);
        //ShowEffect();
    }

}
