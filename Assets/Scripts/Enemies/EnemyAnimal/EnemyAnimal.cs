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
    [Tooltip("Ёффект смерти")]
    public ParticleSystem DieParticleEffect;
    [Tooltip("«вук смерти")]
    [SerializeField] private AudioClip _meDie;

    [Tooltip("—обытие - враг уничтожен")]
    public event Action<EnemyAnimal> EnemyKilled;
    //»сточник звука
    private AudioSource _audioSource;
    // Ёффект получени€ урона
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
        EnemyKilled?.Invoke(this); // —ообщаем подписчикам об убийстве врага
        //PlayDieSound(); // ѕроигрываем звук смерти
        Destroy(enemyHealth.gameObject); // ”дал€ем объект врага со сцены
        // —оздаЄм префаб опыта на месте смерти врага
        GameObject experienceLoot = Instantiate(_experienceLoot, transform.position, transform.rotation);
    }

    private void PlayDieSound()
    {
        _audioSource.clip = _meDie;
        _audioSource.Play();
    }
}

