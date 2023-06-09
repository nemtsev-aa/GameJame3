using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    [Header("Positive Settings")]
    [Tooltip("������� ������� ��������")]
    [SerializeField] private float _experiencePositive = 0f;
    [Tooltip("������ - ��������� ����� ���������� ������")]
    [SerializeField] private ParticleSystem _positiveEffect;
    [Tooltip("���� - ��������� ���������� ������")]
    [SerializeField] private AudioClip _positiveSound;

    [Space(10)]
    [Header("Negative Settings")]
    [Tooltip("������� ������� ��������")]
    [SerializeField] private float _experienceNegative = 0f;
    [Tooltip("������ - ��������� ����� ���������� ������")]
    [SerializeField] private ParticleSystem _negativeEffect;
    [Tooltip("���� - ��������� ���������� ������")]
    [SerializeField] private AudioClip _negativeSound;

    [Tooltip("������������ ������")]
    [SerializeField] private EmotionValueView _emotionValueView;
    [Tooltip("������ ������������ �����")]
    [SerializeField] private AnimationCurve _experienceCurve;

    private AudioSource _audioSource; // �����
    private ParticleSystem _currentParticleSystem;

    private void Start()
    {
        _emotionValueView.ResetValue();
        _audioSource = GetComponent<AudioSource>();
    }

    public void AddPositiveEmotion(float value)
    {
        _experiencePositive += value;
        _audioSource.PlayOneShot(_positiveSound);
        _currentParticleSystem = _positiveEffect;
        
        DisplayExperience();
    }

    public void AddNegativeEmotion(float value)
    {
        _experienceNegative += value;
        _audioSource.PlayOneShot(_negativeSound);
        _currentParticleSystem = _negativeEffect;
        
        DisplayExperience();
    }

    public void DisplayExperience()
    {
        _emotionValueView.NegativeValueShow(_experienceNegative);
        _emotionValueView.PositiveValueShow(_experiencePositive);
        //ShowEffect();
    }

    private void ShowEffect()
    {
        ParticleSystem newEffect = Instantiate(_currentParticleSystem, transform.position + Vector3.up * 3f, Quaternion.identity);
        newEffect.Play();
        //Invoke(nameof(HideEffect), 0.5f);
    }

    //private void HideEffect(ParticleSystem particleSystem)
    //{
    //    _currentParticleSystem.gameObject.SetActive(false);
    //}
}
