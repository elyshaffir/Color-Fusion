using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    public GameObject platform;
    public float platformDistance = 4; // todo this should be calculated from global constants along with other hard coded values

    private float checkCamera; // todo better name
    private float _camSize;

    void Start()
    {
        checkCamera = Camera.main.transform.position.y + platformDistance;
        _camSize = Camera.main.orthographicSize;
    }

    void FixedUpdate()
    {
        if (Camera.main.transform.position.y > checkCamera)
        {
            Instantiate(platform,
                new Vector3(Random.Range(-_camSize, _camSize), checkCamera, 0), // todo hard coded
                transform.rotation);
            checkCamera += platformDistance;
        }
    }
}
