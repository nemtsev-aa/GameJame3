using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectsManager : MonoBehaviour
{
    [Tooltip("������ ����������� ���������� ��������")]
    [SerializeField] private List<ContinuousEffect> _continuousEffectsApplied = new List<ContinuousEffect>();
    [Tooltip("������ ����������� ��������� ��������")]
    [SerializeField] private List<OneTimeEffect> _oneTimeEffectsApplied = new List<OneTimeEffect>();

    [Tooltip("������ ������������� ���������� ��������")]
    [SerializeField] private List<ContinuousEffect> _continuousEffects = new List<ContinuousEffect>();
    [Tooltip("������ ������������� ���������� ��������")]
    [SerializeField] private List<OneTimeEffect> _oneTimeEffects = new List<OneTimeEffect>();

    [SerializeField] private CardsManager _cardsManager;
    [SerializeField] private Player _player;
    [SerializeField] private EnemyManager _enemyManager;

    private void Awake()
    {
        for (int i = 0; i < _continuousEffects.Count; i++) // �������� ���������� ������ �������, ����� �� �������� ���������
        {
            _continuousEffects[i] = Instantiate(_continuousEffects[i]);
            _continuousEffects[i].Initialize(this, _enemyManager, _player);
        }

        for (int i = 0; i < _oneTimeEffects.Count; i++) // �������� ���������� ������ �������, ����� �� �������� ���������
        {
            _oneTimeEffects[i] = Instantiate(_oneTimeEffects[i]);
            _oneTimeEffects[i].Initialize(this, _enemyManager, _player);
        }
    }

    private void Update()
    {
        foreach (var effect in _continuousEffectsApplied) // ��� ������� ������������ ����������� �������
        {
            effect.ProcessFrame(Time.deltaTime); // ������� �������� �������
        }
    }

    [ContextMenu("ShowCards")]
    public void ShowCards()
    {
        List<Effect> effectsToShow = new List<Effect>();    //������ �������� �� ������� ����� ������� 3 ���������

        for (int i = 0; i < _continuousEffectsApplied.Count; i++) // ��������� ������ ����������� ���������� ��������
        {
            if (_continuousEffectsApplied[i].Level < 10)        // ���� ������� ������� ����� 10-�� - ��������� ��� � ������
                effectsToShow.Add(_continuousEffectsApplied[i]);
        }

        for (int i = 0; i < _oneTimeEffectsApplied.Count; i++) // ��������� ������ ����������� ��������� ��������
        {
            if (_oneTimeEffectsApplied[i].Level < 10)        // ���� ������� ������� ����� 10-�� - ��������� ��� � ������
                effectsToShow.Add(_oneTimeEffectsApplied[i]);
        }

        if (_continuousEffectsApplied.Count < 4)        // ���� ���������� ����������� ���������� �������� ������ 4 - ��������� ��� ���������� ���������� ������� � ������
            effectsToShow.AddRange(_continuousEffects);

        if (_oneTimeEffectsApplied.Count < 4)        // ���� ���������� ����������� ��������� �������� ������ 4 - ��������� ��� ���������� ��������� ������� � ������
            effectsToShow.AddRange(_oneTimeEffects);

        int numberOfCardsToShow = Mathf.Min(effectsToShow.Count, 3); // ���������� ����, ������� ����� ��������
        int[] randomIndexes = RandomSort(effectsToShow.Count, numberOfCardsToShow); // ������ ��������� �������� 

        List<Effect> effectsForCards = new List<Effect>(); //������ �������� �� ���������� ��������� ��� ������ � ������� ��������
        for (int i = 0; i < randomIndexes.Length ; i++)
        {
            int index = randomIndexes[i];
            effectsForCards.Add(effectsToShow[index]);
        }

        _cardsManager.ShowCards(effectsForCards);
    }

    private int[] RandomSort(int length, int number)
    {
        int[] array = new int[length];
        for (int i = 0; i < array.Length; i++)
        {
            array[i] = i;
        }

        for (int i = 0; i < array.Length; i++)
        {
            int oldValue = array[i];
            int newIndex = Random.Range(0, array.Length);
            array[i] = array[newIndex];
            array[newIndex] = oldValue;
        }

        int[] result = new int[number];
        for (int i = 0; i < result.Length; i++)
        {
            result[i] = array[i];
        }

        return result;
    }

    public void AddEffect(Effect effect)
    {
        if (effect is ContinuousEffect c_effect) //�������� ������ - ����������
        {
            if (!_continuousEffectsApplied.Contains(c_effect)) // ��������� ������ �� ��������� � ������ ����������� ����������
            {
                _continuousEffectsApplied.Add(c_effect); // ��������� ������ � ������ ����������� ���������� ��������
                _continuousEffects.Remove(c_effect); // ������� ������ �� ������ ������ ���������� ��������
            }
        }
        if (effect is OneTimeEffect o_effect) //�������� ������ - ���������
        {
            if (!_oneTimeEffectsApplied.Contains(o_effect)) // ��������� ������ �� ��������� � ������ ����������� ���������
            {
                _oneTimeEffectsApplied.Add(o_effect); // ��������� ������ � ������ ��������� ����������� ��������
                _oneTimeEffects.Remove(o_effect); // ������� ������ �� ������ ������ ��������� ��������
            }
        }

        effect.Activate(); // ���������� ������
    }
}
