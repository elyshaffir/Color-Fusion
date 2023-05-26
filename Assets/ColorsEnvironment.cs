using UnityEngine;

public class ColorsEnvironment : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public uint numberOfGradients = 1;

    public float yOffset = 0;
    public float textureScale = 1f;
    public Texture2D texture; // todo access from player and enemies

    private float _redSeed;
    private float _greenSeed;
    private float _blueSeed;

    void Start()
    {
        texture = new Texture2D(width, height);

        _redSeed = Random.Range(0f, 1f);
        _greenSeed = Random.Range(0f, 1f);
        _blueSeed = Random.Range(0f, 1f);

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = texture;
    }

    void Update()
    {
        RegenerateTexture();
    }

    private float ColorNoise(float xCoord, float yCoord)
    {
        float sample = Mathf.PerlinNoise(xCoord + _redSeed, yCoord + _redSeed);
        float roundTo = 1.0f / (float)numberOfGradients;
        return Mathf.Round(sample / roundTo) * roundTo;
    }

    private void RegenerateTexture()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                float xCoord = (float)x / width * textureScale;
                float yCoord = (float)y / height * textureScale + yOffset;
                float redSample = ColorNoise(xCoord + _redSeed, yCoord + _redSeed);
                float greenSample = ColorNoise(xCoord + _greenSeed, yCoord + _greenSeed);
                float blueSample = ColorNoise(xCoord + _blueSeed, yCoord + _blueSeed);
                Color color = new Color(redSample, greenSample, blueSample);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
    }
}
