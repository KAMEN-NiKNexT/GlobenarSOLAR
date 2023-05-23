using UnityEngine;

public class InfiniteScrollManager : MonoBehaviour
{
    public GameObject[] objectsToScroll; // Массив объектов, подлежащих скроллингу
    public float scrollSpeed = 1f;
    public float resetPosition = -10f;
    public float startPosition = 0f;

    private int poolSize; // Размер пула объектов
    private float scrollOffset; // Смещение скролла

    private void Start()
    {
        poolSize = objectsToScroll.Length;
        scrollOffset = Mathf.Abs(resetPosition - startPosition) / poolSize;

        // Располагаем объекты в начальных позициях
        for (int i = 0; i < poolSize; i++)
        {
            objectsToScroll[i].transform.position = new Vector3(0f, startPosition + i * scrollOffset, 0f);
        }
    }

    private void Update()
    {
        // Изменяем положение контейнера с объектами
        transform.position += new Vector3(0f, scrollSpeed * Time.deltaTime, 0f);

        // Проверяем, достиг ли скролл определенной позиции для переиспользования объектов из пула
        if (transform.position.y < resetPosition)
        {
            // Перемещаем текущий объект в конец пула
            GameObject objToMove = objectsToScroll[0];
            objectsToScroll[0] = objectsToScroll[poolSize - 1];
            objectsToScroll[poolSize - 1] = objToMove;

            // Обновляем позицию перемещенного объекта
            objectsToScroll[poolSize - 1].transform.position = new Vector3(0f, objectsToScroll[poolSize - 2].transform.position.y + scrollOffset, 0f);
        }
    }
}