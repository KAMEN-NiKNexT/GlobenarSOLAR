using UnityEngine;

public class InfiniteScrollManager : MonoBehaviour
{
    public GameObject[] objectsToScroll; // ������ ��������, ���������� ����������
    public float scrollSpeed = 1f;
    public float resetPosition = -10f;
    public float startPosition = 0f;

    private int poolSize; // ������ ���� ��������
    private float scrollOffset; // �������� �������

    private void Start()
    {
        poolSize = objectsToScroll.Length;
        scrollOffset = Mathf.Abs(resetPosition - startPosition) / poolSize;

        // ����������� ������� � ��������� ��������
        for (int i = 0; i < poolSize; i++)
        {
            objectsToScroll[i].transform.position = new Vector3(0f, startPosition + i * scrollOffset, 0f);
        }
    }

    private void Update()
    {
        // �������� ��������� ���������� � ���������
        transform.position += new Vector3(0f, scrollSpeed * Time.deltaTime, 0f);

        // ���������, ������ �� ������ ������������ ������� ��� ����������������� �������� �� ����
        if (transform.position.y < resetPosition)
        {
            // ���������� ������� ������ � ����� ����
            GameObject objToMove = objectsToScroll[0];
            objectsToScroll[0] = objectsToScroll[poolSize - 1];
            objectsToScroll[poolSize - 1] = objToMove;

            // ��������� ������� ������������� �������
            objectsToScroll[poolSize - 1].transform.position = new Vector3(0f, objectsToScroll[poolSize - 2].transform.position.y + scrollOffset, 0f);
        }
    }
}