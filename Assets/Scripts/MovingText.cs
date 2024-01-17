using UnityEngine;

public class MovingText : MonoBehaviour
{
    public float moveSpeed = 2.0f;
    public float moveRange = 5.0f;
    public float rotationSpeed = 20.0f;

    private Vector3 initialPosition;

    void Start()
    {
        initialPosition = transform.position;
    }

    void Update()
    {
        float x = initialPosition.x + Mathf.Sin(Time.time * moveSpeed) * moveRange;
        float y = initialPosition.y + Mathf.Cos(Time.time * moveSpeed) * moveRange;

        transform.position = new Vector3(x, y, initialPosition.z);
        transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
    }
}
