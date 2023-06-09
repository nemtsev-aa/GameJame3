using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowMissle : MonoBehaviour
{
    [Tooltip("���������� ���� �������")]
    [SerializeField] private Rigidbody _rigidbody;
    [Tooltip("��������� �������")]
    [SerializeField] private Collider _collider;
    [Tooltip("���������� ������")]
    [SerializeField] private ParticleSystem _particleSystem;

    private float _damage; // �������� �����
    private float _passCount; // ���������� ������������� �����
    
    private void Start()
    {
        Destroy(gameObject, 5f);
    }
    
    public void Setup(Vector3 velocity, float damage, int passCount)
    {
        transform.rotation = Quaternion.LookRotation(velocity);
        _damage = damage;
        _rigidbody.velocity = velocity;
        _passCount = passCount;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<EnemyHealth>() is EnemyHealth enemy)
        {
            enemy.TakeDamage(_damage);
            _passCount--;
            if (_passCount == 0)
                Die();         
        }
    }

    private void Die()
    {
        _rigidbody.velocity = Vector3.zero;
        _collider.enabled = false;
        _particleSystem.Stop();
        Destroy(gameObject, 2f);
    }
}
