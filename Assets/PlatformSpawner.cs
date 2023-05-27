using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private Transform _mainGuy;
    [SerializeField] private GameObject _platformPrefab;
    private int PLATFORM_CREATION_AMOUNT = 30;
    private double PLATFORM_HEIGHT = 0.3;

    private void Start()
    {
        Vector3 cameraBottomPosition = new Vector3(transform.position.x,
            Camera.main.transform.position.y - Camera.main.orthographicSize, transform.position.z);
        CreatePlatformBulk(cameraBottomPosition);
    }

    private void Update()
    {

    }

    private void CreatePlatformBulk(Vector3 lastBlockPosition)
    {
        float cameraLeftEdge = Camera.main.transform.position.x - Camera.main.transform.localScale.x;
        float cameraRightEdge = Camera.main.transform.position.x + Camera.main.transform.localScale.x;

        for (int i = 0; i < PLATFORM_CREATION_AMOUNT; i++)
        {
            float platfromSize = Random.Range(1, 4);
            float platfromPositionX = Random.Range(cameraLeftEdge, cameraRightEdge);
            float platformPositionY = lastBlockPosition.y + Random.Range(1, 2);

            Vector3 platformPosition = new Vector3(platfromPositionX, platformPositionY, transform.position.z);

            GameObject platform = Instantiate(_platformPrefab, platformPosition, Quaternion.identity);
            platform.transform.localScale = new Vector3(platfromSize, (float)PLATFORM_HEIGHT, transform.localScale.z);

        }
    }
}
