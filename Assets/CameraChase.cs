using UnityEngine;

public class CameraChase : MonoBehaviour
{
    public float maxSpeed = 0.05f;
    public ColorsEnvironment environment;

    private float _currentSpeed = 0.001f;

    void FixedUpdate()
    {
        Transform transform = GetComponent<Transform>();

        _currentSpeed = Mathf.Min(_currentSpeed + 0.001f, maxSpeed);

        transform.position = new Vector3(transform.position.x,
            transform.position.y + _currentSpeed,
            transform.position.z);
    }
}
