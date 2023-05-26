using UnityEngine;

public class CameraChase : MonoBehaviour
{
    public float maxSpeed = 0.05f;
    public ColorsEnvironment environment;

    private float currentSpeed = 0.001f;

    void FixedUpdate()
    {
        Transform transform = GetComponent<Transform>();

        currentSpeed = Mathf.Min(currentSpeed + 0.001f, maxSpeed);

        transform.position = new Vector3(transform.position.x,
            transform.position.y + currentSpeed,
            transform.position.z);

        // todo why does /10 fixes speed of environment?
        // maybe the scale of the quad?
        environment.yOffset += currentSpeed / 10.0f;
    }
}
