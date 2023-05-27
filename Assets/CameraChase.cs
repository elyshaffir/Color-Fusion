using UnityEngine;

public class CameraChase : MonoBehaviour
{
    [SerializeField] private Transform _mainGuy;

   // public float maxSpeed = 0.02f;

    private float _currentSpeed = 0.015f;

    private void LateUpdate()
    {
        if (_mainGuy.position.y > transform.position.y)
        {
            Vector3 newPosition = new Vector3(transform.position.x, _mainGuy.position.y, transform.position.z);
            transform.position = newPosition;
        }
    }
    private void FixedUpdate()
    {
        Transform transform = GetComponent<Transform>();

        // _currentSpeed = Mathf.Min(_currentSpeed + 0.001f, maxSpeed);

        transform.position = new Vector3(transform.position.x,
            transform.position.y + _currentSpeed,
            transform.position.z);
    }
}
