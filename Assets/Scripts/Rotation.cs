using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float speed = 5f; // �������� ��������

    private void Update()
    {
        // ������������ ������ ������ ����� ��� �� �������� * deltaTime �������� � �������
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}