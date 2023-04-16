using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public float speed = 5f; // �������� ��������
    public float radius = 2f; // ������ �����
    public bool clockwise = true; // ����������� �������� �� ����� (�� ������� ��� ������)
    [SerializeField] private GameObject _target;

    private Vector3 center; // ����� �����
    private float angle; // ���� ��������

    private void Start()
    {
        center = _target.transform.position; // ����� ����� ����� ��������� ������� �������
    }

    private void Update()
    {
        // �������� ���� �������� � ����������� �� ����������� �������� � ��������
        angle += clockwise ? speed * Time.deltaTime : -speed * Time.deltaTime;
        // ��������� ����� ������� �� ������ ������, ������� � ���� ��������
        var newPosition = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
        transform.position = newPosition;
    }
}