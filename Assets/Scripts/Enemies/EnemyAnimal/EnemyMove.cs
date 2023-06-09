using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Tooltip("���������� ���� �����")]
    [SerializeField] private Rigidbody _rigidbody;
    [Tooltip("C�������")]
    [SerializeField] private float _speed = 3f;
    [Tooltip("C������� ��������")]
    [SerializeField] private float _rotationLerpRate = 3f;

    private Transform _targetTransform; // ��������� ����
    private EnemyManager _enemyManager; // �������� ������
    
    public void Setup(Transform playerTransform, EnemyManager enemyManager)
    {

        _targetTransform = playerTransform;
        _enemyManager = enemyManager;
    }

    private void Update()
    {
        Vector3 toTarget = _targetTransform.position - transform.position; // ������ �� �������� ��������� � ����
        if (toTarget.magnitude > 30f)
        {
            EnemyAnimal enemyAnimal = gameObject.GetComponent<EnemyAnimal>();
            _enemyManager.RemoveEnemy(enemyAnimal);
            Destroy(enemyAnimal.gameObject);
        }

        Quaternion targetRotation = Quaternion.LookRotation(toTarget, Vector3.up); // ������� ���� ��������
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationLerpRate); // ������� ����� � ����
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.forward * _speed; // ���� ��� �������������
    }
}
