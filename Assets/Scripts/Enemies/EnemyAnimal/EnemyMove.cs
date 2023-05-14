using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [Tooltip("Физическое тело врага")]
    [SerializeField] private Rigidbody _rigidbody;
    [Tooltip("Cкорость")]
    [SerializeField] private float _speed = 3f;
    [Tooltip("Cкорость поворота")]
    [SerializeField] private float _rotationLerpRate = 3f;

    private Transform _targetTransform; // Трансформ цели
    private EnemyManager _enemyManager; // Менеджер эмоций
    
    public void Setup(Transform playerTransform, EnemyManager enemyManager)
    {

        _targetTransform = playerTransform;
        _enemyManager = enemyManager;
    }

    private void Update()
    {
        Vector3 toTarget = _targetTransform.position - transform.position; // Вектор от текущего положения к цели
        if (toTarget.magnitude > 30f)
        {
            EnemyAnimal enemyAnimal = gameObject.GetComponent<EnemyAnimal>();
            _enemyManager.RemoveEnemy(enemyAnimal);
        }

        Quaternion targetRotation = Quaternion.LookRotation(toTarget, Vector3.up); // Целевой угол поворота
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _rotationLerpRate); // Поворот врага к цели
    }

    private void FixedUpdate()
    {
        _rigidbody.velocity = transform.forward * _speed; // Сила для преследования
    }
}
