using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    [Tooltip("���������")]
    [SerializeField] private Collider _collider;
    [Tooltip("����� ����� � ��������")]
    [SerializeField] private float _timeToCollector;

    private Transform _textPosition;
    
    public void Collect(Collector collector, Transform textPosition)
    {
        _textPosition = textPosition;
        _collider.enabled = false; // ��������� ��������� ����, ����� ���������� ����������� ����
        StartCoroutine(MoveToCollector(collector)); // ������������� ����
    }

    private IEnumerator MoveToCollector(Collector collector)
    {
        Vector3 a = transform.position; // ������� ��������� ����
        Vector3 b = a + Vector3.up * 4f; // �������� �1 ��� �������� ������ �����
        Vector3 d = _textPosition.position; // ������� ��������� ��������
        Vector3 c = d + Vector3.up * 2f; // �������� �2 ��� �������� ������ �����


        for (float t = 0; t < 1f; t+=Time.deltaTime / _timeToCollector)
        {
            Vector3 position = Bezier.GetPoint(a, b, c, d, t);
            transform.position = position; // ����� ��������� ���� �� ���� � ��������
            yield return null;
        }
        Take(collector);
    }

    public virtual void Take(Collector collector)
    {
        Destroy(gameObject); // ���������� ��� ����� �����
    }
}
