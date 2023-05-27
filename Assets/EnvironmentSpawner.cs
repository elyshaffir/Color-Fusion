using System.Collections.Generic;
using UnityEngine;

public class EnvironmentSpawner : MonoBehaviour
{
    public GameObject environmentPrefab;

    public int width = 256; // todo this can be increased a lot
    public int height = 256;
    public uint numberOfGradients = 1; // todo remove
    public float textureScale = 1f;

    private List<GameObject> environments = new List<GameObject>();

    private float _seed;
    private float nextYSpawn;
    private float _spawnYInterval;

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
    }

    // Update is called once per frame
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
        newEnvironment.GetComponent<ColorsEnvironment>().width = width;
        newEnvironment.GetComponent<ColorsEnvironment>().height = height;
        environments.Add(newEnvironment);
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
