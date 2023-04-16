using UnityEngine;

public class Rotation : MonoBehaviour
{
    public float speed = 5f; // скорость вращения

    private void Update()
    {
        // поворачиваем объект вокруг своей оси на скорость * deltaTime градусов в секунду
        transform.Rotate(Vector3.up * speed * Time.deltaTime);
    }
}