using UnityEngine;

public class CameraChase : MonoBehaviour
{
    [SerializeField] private Transform _mainGuy;

    private void LateUpdate()
    {
        if (_mainGuy.position.y > transform.position.y)
        {
            Vector3 newPosition = new Vector3(transform.position.x, _mainGuy.position.y, transform.position.z);
            transform.position = newPosition;
        }
    }
}
