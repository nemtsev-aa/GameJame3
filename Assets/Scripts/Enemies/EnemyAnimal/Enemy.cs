using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("�������� ����������")]
    [field: SerializeField] public EnemyHealth EnemyHealth { get; private set; }
    [Tooltip("������ - ����")]
    [field: SerializeField] public GameObject _experienceLoot;
    
    private float _attackTimer;
    private PlayerHealth _playerHealth;
    private EnemyMove _enemyMove;

    private void Awake()
    {
        _enemyMove = gameObject.GetComponent<EnemyMove>();
        _enemyMove.enabled = false;
    }
    
    public void Init(Transform playerTransform, EnemyManager enemyManager)
    {
        _enemyMove.Setup(playerTransform, enemyManager);
        Invoke(nameof(MoveStart), 2f);
    }

    private void MoveStart()
    {
        _enemyMove.enabled = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() is PlayerHealth playerHealth)
        {
            _playerHealth = playerHealth;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<PlayerHealth>() is PlayerHealth playerHealth)
        {
            _playerHealth = null;
        }
    }
}
