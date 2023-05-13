using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Tooltip("Здоровье противника")]
    [field: SerializeField] public EnemyHealth EnemyHealth { get; private set; }
    [Tooltip("Префаб - опыт")]
    [field: SerializeField] public GameObject _experienceLoot;
    
    private float _attackTimer;
    private PlayerHealth _playerHealth;
    private EnemyMove _enemyMove;

    private void Awake()
    {
        _enemyMove = gameObject.GetComponent<EnemyMove>();
    }
    
    public void Init(Transform playerTransform, EnemyManager enemyManager)
    {
        _enemyMove.Setup(playerTransform, enemyManager);
    }

    //private void Update()
    //{
    //    if (_playerHealth)
    //    {
    //        _attackTimer += Time.deltaTime;
    //        if (_attackTimer > _attackPeriod)
    //        {
    //            _playerHealth.TakeDamage(_dps * _attackPeriod);
    //            _attackTimer = 0;
    //        }    
    //    }
    //}

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
