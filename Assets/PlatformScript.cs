using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    void Update()
    {
        if (LogicScript.isOutOfScreen(transform.position.y))
        {
            Destroy(gameObject);
        }
    }
}
