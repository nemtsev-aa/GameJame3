using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ExperienceManager : MonoBehaviour
{
    [Tooltip("������� ������� ��������")]
    [SerializeField] private float _experienceNegative = 0f;
    [Tooltip("������� ������� ��������")]
    [SerializeField] private float _experiencePositive = 0f;

    [Tooltip("������ ������������ �����")]
    [SerializeField] private AnimationCurve _experienceCurve;
    [Tooltip("������ - ��������� ����� ���������� ������")]
    [SerializeField] private ParticleSystem _negativeEffect;
    [Tooltip("������ - ��������� ����� ���������� ������")]
    [SerializeField] private ParticleSystem _positiveEffect;

    [Tooltip("���� - ��������� ���������� ������")]
    [SerializeField] private AudioClip _negativeSound;
    [Tooltip("���� - ��������� ���������� ������")]
    [SerializeField] private AudioClip _positiveSound;

    [Tooltip("������������ ������")]
    [SerializeField] private EmotionValueView _emotionValueView;


    private int _level = 0; // ������� �������
    private AudioSource _audioSource; // �����

    private void Start()
    {
        _emotionValueView.ResetValue();
    }

    public void AddPositiveEmotion(float value)
    {
        _experiencePositive += value;
        _audioSource.PlayOneShot(_positiveSound);

        DisplayExperience();
    }

    public void AddNegativeEmotion(float value)
    {
        _experienceNegative += value;
        _audioSource.PlayOneShot(_negativeSound);

        DisplayExperience();
    }

    public void UpLevel()
    {
        //_level++;
        //ShowEffectToLevelUp();
        //_experience = 0;
        //_nextLevelExperience = _experienceCurve.Evaluate(_level);

        //Invoke(nameof(ShowCards), 2f);
    }

    public void DisplayExperience()
    {
        _emotionValueView.NegativeValueShow(_experienceNegative);
        _emotionValueView.PositiveValueShow(_experiencePositive);
    }

    private void ShowEffectToLevelUp()
    {
        //_upEffect.Play();
        //_levelText.text = _level.ToString();
        //_audioSource.PlayOneShot(_levelUpSound);
    }

    //private void ShowCards()
    //{
    //    _effectsManager.ShowCards();
    //}
}
