using UnityEngine;

public class PlatformSpawn : MonoBehaviour
{
    public GameObject platform;
    public float platformDistance = 4; // todo this should be calculated from global constants along with other hard coded values

    private float _checkCamera; // todo better name

    void Start()
    {
        _checkCamera = Camera.main.transform.position.y + platformDistance;
    }

    void FixedUpdate()
    {
        if (Camera.main.transform.position.y > _checkCamera)
        {
            Instantiate(platform,
                new Vector3(Random.Range(-5, 5), _checkCamera, 0), // todo hard coded
                transform.rotation);
            _checkCamera += platformDistance;
        } 
    }
}
