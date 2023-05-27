using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    public MainGuy player;
    public GameObject environmentPrefab;
    public List<GameObject> environments;

    public int width = 256;
    public int height = 256;
    public uint numberOfGradients = 1;
    public float textureScale = 1f;

    private float _redSeed;
    private float _yellowSeed;
    private float _blueSeed;

    private float nextYSpawn;
    private float _spawnYInterval;

    void Start()
    {
        _redSeed = Random.Range(0f, 1f);
        _yellowSeed = Random.Range(0f, 1f);
        _blueSeed = Random.Range(0f, 1f);
        nextYSpawn = Camera.main.transform.position.y;
        _spawnYInterval = environmentPrefab.transform.localScale.y;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Camera.main.transform.position.y >= nextYSpawn - _spawnYInterval)
        {
            SpawnEnvironment();
            nextYSpawn += _spawnYInterval;
        }
    }

    void SpawnEnvironment()
    {
        // todo remove environment after out of view after
        GameObject newEnvironment = Instantiate(environmentPrefab,
            new Vector3(Camera.main.transform.position.x,
                        nextYSpawn,
                        player.transform.position.z),
            transform.rotation);
        Texture2D generatedTexture = GenerateTexture();
        newEnvironment.GetComponent<ColorsEnvironment>().texture = generatedTexture;
        newEnvironment.GetComponent<Renderer>().material.mainTexture = generatedTexture;
        newEnvironment.GetComponent<ColorsEnvironment>().width = width;
        newEnvironment.GetComponent<ColorsEnvironment>().height = height;
        environments.Add(newEnvironment);
    }

    private float ColorNoise(float xCoord, float yCoord)
    {
        float sample = Mathf.PerlinNoise(xCoord, yCoord);
        float roundTo = 1.0f / (float)numberOfGradients;
        return Mathf.Round(sample / roundTo) * roundTo;
    }

    private Texture2D GenerateTexture()
    {
        Texture2D texture = new Texture2D(width, height);
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * textureScale;
                float yCoord = (float)y / height * textureScale + (nextYSpawn / 10.0f); // todo hard coded
                float redSample = ColorNoise(xCoord + _redSeed, yCoord + _redSeed);
                float yellowSample = ColorNoise(xCoord + _yellowSeed, yCoord + _yellowSeed);
                float blueSample = ColorNoise(xCoord + _blueSeed, yCoord + _blueSeed);
                Color color = new Color(redSample + yellowSample, yellowSample, blueSample);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
        return texture;
    }
}
