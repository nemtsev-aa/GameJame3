using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [Tooltip("Положение игрока")]
    [SerializeField] private Transform _playerTransform;
    [Tooltip("Зона создания позитивных эмоций")]
    [SerializeField] private Transform _positiveCreateZone;
    [Tooltip("Зона создания негативных")]
    [SerializeField] private Transform _negativeCreateZone;
    [Tooltip("Радиус создания врагов")]
    [SerializeField] private float _creationRadius;
    [Tooltip("Данные для создания врагов")]
    [SerializeField] private ChapterSettings _chapterSettings;
    [Tooltip("Список созданных врагов")]
    [SerializeField] private List<EnemyAnimal> _enemyList = new List<EnemyAnimal>();

    [SerializeField] private float _timeToNewWave = 10f;

    private float _time;
    private int _currentWave;

    //void Update()
    //{
    //    _time += Time.deltaTime;
    //    if (_time >= _timeToNewWave || _enemyList.Count == 0)
    //    {
    //        _time = 0;
    //        _currentWave++;
    //        StartNewWave(_currentWave);
    //    }
    //}

    public void StartNewWave(int wave)
    {
        _currentWave = wave;

        StopAllCoroutines(); // Останавливаем все запущенные корутины перед стартом новой
        for (int i = 0; i < _chapterSettings.EnemyWavesArray.Length; i++) // Проходим во всем элементам массива врагов
        {
            EnemyAnimal iEnemy = _chapterSettings.EnemyWavesArray[i].Enemy; // i-й враг
            float iEnemyNumber = _chapterSettings.EnemyWavesArray[i].NumberPerSecond[wave]; // Периодичность создания врагов в первой волне
            if (iEnemyNumber > 0) // Если создание врагов не запланировано
                StartCoroutine(CreateEnemyInSeconds(iEnemy, iEnemyNumber));
        }
    }

    private IEnumerator CreateEnemyInSeconds(EnemyAnimal enemy, float enemyPerSecond)
    {
        while (true) // Бесконечное выполнение
        {
            yield return new WaitForSeconds(1f / enemyPerSecond); // Перерыв, соответствующий каждой волне и врагу  
            Create(enemy);
        }
    }

    public void Create(EnemyAnimal enemy)
    {
        //Vector2 randomPoint = Random.insideUnitCircle.normalized; // Случайная точка на единичной окружности
        //Vector3 position = new Vector3(randomPoint.x, 0f, randomPoint.y) * _creationRadius + _playerTransform.position; // Положение врага при создании

        EnemyAnimal newEnemy = new();
        if (enemy._experienceLoot.GetComponent<ExperienceLoot>() is ExperienceLoot loot)
        {
            if (loot.EmotionType == EmotionType.Positive)
                newEnemy = Instantiate(enemy, GetPointInCreatePositiveZone(), Quaternion.identity); // Новый враг в расчитанном положении
            else
                newEnemy = Instantiate(enemy, GetPointInCreateNegativeZone(), Quaternion.identity); // Новый враг в расчитанном положении
        }

        newEnemy.transform.LookAt(_playerTransform);
        newEnemy.Init(_playerTransform, this); // Передаём врагу сведения о положении игрока
        newEnemy.EnemyKilled += RemoveEnemy;
        _enemyList.Add(newEnemy); // Добавляем врага в список
    }

    private Vector3 GetPointInCreatePositiveZone()
    {
        float x = Random.Range(-0.5f, 0.5f);
        float y = 0f;
        float z = Random.Range(-0.5f, 0.5f);

        return _positiveCreateZone.TransformPoint(x, y, z);

    }
    private Vector3 GetPointInCreateNegativeZone()
    {
        float x = Random.Range(-0.5f, 0.5f);
        float y = 0f;
        float z = Random.Range(-0.5f, 0.5f);

        return _negativeCreateZone.TransformPoint(x, y, z);

    }

    public void RemoveEnemy(EnemyAnimal enemy)
    {
        _enemyList.Remove(enemy); // Удаляем врага из списка
    }

    public EnemyAnimal[] GetNearest(Vector3 point, int number)
    {
        _enemyList = _enemyList.OrderBy(x => Vector3.Distance(point, x.transform.position)).ToList();
        int returnNumber = Mathf.Min(number, _enemyList.Count);
        EnemyAnimal[] enemies = new EnemyAnimal[returnNumber];
        for (int i = 0; i < returnNumber; i++)
        {
            enemies[i] = _enemyList[i];
        }
        return enemies;
    }

    private void OnDrawGizmos()
    {
#if UNITY_EDITOR
        Handles.color = Color.red;
        Handles.DrawWireDisc(_playerTransform.position, Vector3.up, _creationRadius);
        Gizmos.DrawLine(transform.position, Vector3.zero - transform.position);
#endif
    }


}
