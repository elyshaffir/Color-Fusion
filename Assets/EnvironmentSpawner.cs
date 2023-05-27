using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    public GameObject environmentPrefab;

    public Vector2Int resolutionMultiplier = new Vector2Int(20, 20);
    public float noiseScale = 1f;

    private List<GameObject> environments = new List<GameObject>();

    private float _seed;
    private float nextYSpawn;
    private float _spawnYInterval;
    private Vector2 _environmentScale;
    private Vector2Int _environmentResolution;

    public bool TryGetEnvironmentByLocation(Vector3 location, out ColorsEnvironment outEnvironment)
    {
        foreach (GameObject environment in environments)
        {
            if (environment.transform.position.y - (environment.transform.localScale.y / 2) < location.y &&
                environment.transform.position.y + (environment.transform.localScale.y / 2) > location.y)
            {
                outEnvironment = environment.GetComponent<ColorsEnvironment>();
                return true;
            }
        }
        outEnvironment = new ColorsEnvironment();
        return false;
    }

    void Start()
    {
        _seed = Random.Range(0f, 1f);
        nextYSpawn = Camera.main.transform.position.y;
        _spawnYInterval = environmentPrefab.transform.localScale.y;
        _environmentScale = 2 *
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
        _environmentResolution = new Vector2Int((int)_environmentScale.x, (int)_environmentScale.y) * resolutionMultiplier;
    }

    void FixedUpdate()
    {
        if (Camera.main.transform.position.y >= nextYSpawn - _spawnYInterval)
        {
            SpawnEnvironment();
            nextYSpawn += _spawnYInterval;
        }
        for (int i = environments.Count - 1; i >= 0; i--)
        {
            GameObject environmentToRemove = environments[i];
            if (LogicScript.isOutOfScreen(environmentToRemove.transform.position.y + environmentToRemove.transform.localScale.y))
            {
                environments.RemoveAt(i);
                Destroy(environmentToRemove);
            }
        }
    }

    void SpawnEnvironment()
    {
        GameObject newEnvironment = Instantiate(environmentPrefab,
            new Vector3(Camera.main.transform.position.x,
                        nextYSpawn,
                        environmentPrefab.transform.position.z),
            transform.rotation);
        Texture2D generatedTexture = GenerateTexture();
        newEnvironment.GetComponent<ColorsEnvironment>().texture = generatedTexture;
        newEnvironment.GetComponent<Renderer>().material.mainTexture = generatedTexture;
        newEnvironment.GetComponent<ColorsEnvironment>().width = _environmentResolution.x;
        newEnvironment.GetComponent<ColorsEnvironment>().height = _environmentResolution.y;

        newEnvironment.transform.localScale =
            new Vector3(_environmentScale.x, _environmentScale.y, newEnvironment.transform.localScale.z);
        environments.Add(newEnvironment);
    }

    private Texture2D GenerateTexture()
    {
        Debug.Log(_environmentResolution);
        Texture2D texture = new Texture2D(_environmentResolution.x, _environmentResolution.y);
        for (int x = 0; x < _environmentResolution.x; x++)
        {
            for (int y = 0; y < _environmentResolution.y; y++)
            {
                float xCoord = (float)x / _environmentResolution.x * noiseScale;
                float yCoord = (float)y / _environmentResolution.y * noiseScale + (nextYSpawn / 10.0f); // todo hard coded
                float sample = Mathf.PerlinNoise(xCoord + _seed, yCoord + _seed);
                Color color;
                if (sample < 0.3f)
                {
                    color = Colors.red;
                }
                else if (sample < 0.6f)
                {
                    color = Colors.yellow;
                }
                else
                {
                    color = Colors.blue;
                }
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }
}
