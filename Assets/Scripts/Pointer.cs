using UnityEngine;

public class Pointer : MonoBehaviour
{
    [Tooltip("������ �������")]
    [SerializeField] private Transform _aim;
    [Tooltip("������ ������")]
    [SerializeField] private Camera _playerCamera;

    void LateUpdate()
    {
        // ��� �� ������ ������ � ������� ������� ���� �� ������
        Ray ray = _playerCamera.ScreenPointToRay(Input.mousePosition);
        // ������������ ���� � �����
        Debug.DrawRay(ray.origin, ray.direction * 60f, Color.yellow);
        // ��������� � ������� �������� ������� �������
        Plane plane = new Plane(-Vector3.up, Vector3.zero);

        // ���������� �� ������ �� ���������
        float distance;
        // �������� ���������� �� ������ �� ��������� � ������� ���������� ����
        plane.Raycast(ray, out distance);
        // ����� ����������� ���� � ���������
        Vector3 point = ray.GetPoint(distance);
        // ���������� ������ � ���� ����������� ���� � ���������
        _aim.position = point;
        // ������������� �������� � ����������� �������
        Vector3 toAim = _aim.position - transform.position;
        transform.rotation = Quaternion.LookRotation(toAim);
    }

    public void GetTransform(Transform transform)
    {
        _aim = transform;
    }
}
