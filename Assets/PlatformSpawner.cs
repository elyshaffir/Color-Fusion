using UnityEngine;

public class PlatformSpawner : MonoBehaviour
{
    [SerializeField] private Transform _mainGuy;
    [SerializeField] private GameObject _platformPrefab;
    [SerializeField] private GameObject _enemyPrefab;
    private Vector3 lastBlockInPreviousBulkPosition;
    private float _cameraLeftEdge;
    private float _cameraRightEdge;
    private int PLATFORM_CREATION_AMOUNT = 10;
    private double PLATFORM_HEIGHT = 0.3;

    private void Start()
    {
        Vector2 screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _cameraLeftEdge = screenBounds.x * (-1);
        _cameraRightEdge = screenBounds.x;
        lastBlockInPreviousBulkPosition = CreatePlatformBulk(Camera.main.transform.position);
    }

    private void Update()
    {
        if (LogicScript.isGameActive)
        {
            if (lastBlockInPreviousBulkPosition.y - _mainGuy.transform.position.y <= 20)
            {
                lastBlockInPreviousBulkPosition = CreatePlatformBulk(lastBlockInPreviousBulkPosition);
            }
        }
    }

    private Vector3 CreatePlatformBulk(Vector3 lastBlockInPreviousBulkPosition)
    {
        Vector3 previousBlockPosition = new Vector3();
        int platformsWithoutEnemy = 0;
        int platformsUntilEnemy = Random.Range(3,5);
        for (int i = 0; i < PLATFORM_CREATION_AMOUNT; i++)
        {
            float platfromSize = Random.Range(4f, 8f);
            float platfromPositionX = Random.Range(_cameraLeftEdge, _cameraRightEdge);
            float platformPositionY = GetNewYPosition(i == 0 ? lastBlockInPreviousBulkPosition.y : previousBlockPosition.y);

            Vector3 platformPosition = new Vector3(platfromPositionX, platformPositionY, transform.position.z);

            GameObject platform = Instantiate(_platformPrefab, platformPosition, Quaternion.identity);
            platform.transform.localScale = new Vector3(platfromSize, (float)PLATFORM_HEIGHT, transform.localScale.z);

            if (platformsWithoutEnemy >= platformsUntilEnemy)
            {
                //create enemy on platform
                Vector3 enemyStartPosition = new Vector3(platformPosition.x, platformPosition.y + 1, transform.position.z);
                float platformRightSidePositionX = platfromPositionX + platfromSize / 2;
                float platformLeftSidePositionX = platfromPositionX - platfromSize / 2;
                GameObject enemy = Instantiate(_enemyPrefab, platformPosition, Quaternion.identity);
                enemy.GetComponent<Enemy>().Init(enemyStartPosition, platformLeftSidePositionX, platformRightSidePositionX);

                platformsWithoutEnemy = 0;
                platformsUntilEnemy = Random.Range(3, 5);
            } 
            else
            {
                platformsWithoutEnemy++;
            } 

            previousBlockPosition = platform.transform.localPosition;
        }

        return previousBlockPosition;
    }

    private float GetNewYPosition(float previousY)
    {
        return previousY + Random.Range(2f, 3.5f);
    }
}
