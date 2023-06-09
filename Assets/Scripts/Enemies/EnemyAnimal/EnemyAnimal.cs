#if UNITY_EDITOR
using UnityEditor;
#endif  

using System;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyAnimalType
{
    Hen,
    Rabbit,
    Pig,
    Squirrel,
    Bear,
    Spike,
    Shell,
    HermitKing
}

public class EnemyAnimal : Enemy
{
    [Tooltip("������ ������")]
    public ParticleSystem DieParticleEffect;
    [Tooltip("���� ������")]
    [SerializeField] private AudioClip _meDie;

    [Tooltip("������� - ���� ���������")]
    public event Action<EnemyAnimal> EnemyKilled;
    //�������� �����
    private AudioSource _audioSource;
    // ������ ��������� �����
    private BlinkEffect _blinkEffect;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _blinkEffect = GetComponent<BlinkEffect>();
    }

    private void OnEnable()
    {
        EnemyHealth.HealthIsOver += Die;
    }

    private void OnDisable()
    {
        EnemyHealth.HealthIsOver -= Die;
    }

    private void Die(EnemyHealth enemyHealth)
    {
        EnemyKilled?.Invoke(this); // �������� ����������� �� �������� �����
        //PlayDieSound(); // ����������� ���� ������
        Destroy(enemyHealth.gameObject); // ������� ������ ����� �� �����
        // ������ ������ ����� �� ����� ������ �����
        GameObject experienceLoot = Instantiate(_experienceLoot, transform.position, transform.rotation);
    }

    private void PlayDieSound()
    {
        _audioSource.clip = _meDie;
        _audioSource.Play();
    }
}

