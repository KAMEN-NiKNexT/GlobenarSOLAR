using UnityEngine;

public class CircleMovement : MonoBehaviour
{
    public float speed = 5f; // скорость движения
    public float radius = 2f; // радиус круга
    public bool clockwise = true; // направление движения по кругу (по часовой или против)
    [SerializeField] private GameObject _target;

    private Vector3 center; // центр круга
    private float angle; // угол поворота

    private void Start()
    {
        center = _target.transform.position; // центр круга равен начальной позиции объекта
    }

    private void Update()
    {
        // изменяем угол поворота в зависимости от направления движения и скорости
        angle += clockwise ? speed * Time.deltaTime : -speed * Time.deltaTime;
        // вычисляем новую позицию на основе центра, радиуса и угла поворота
        var newPosition = center + new Vector3(Mathf.Cos(angle), 0, Mathf.Sin(angle)) * radius;
        transform.position = newPosition;
    }
}