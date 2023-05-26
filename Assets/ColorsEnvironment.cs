using UnityEngine;

public class ColorsEnvironment : MonoBehaviour
{
    public int width = 256;
    public int height = 256;
    public int numberOfGradients = 1;

    public float yOffset = 0;
    public float textureScale = 1f;
    public Texture2D texture; // todo access from player and enemies

    private float redSeed;
    private float greenSeed;
    private float blueSeed;

    void Start()
    {
        texture = new Texture2D(width, height);

        redSeed = Random.Range(0f, 1f);
        greenSeed = Random.Range(0f, 1f);
        blueSeed = Random.Range(0f, 1f);

        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = texture;
    }

    void Update()
    {
        RegenerateTexture();
    }

    private float ColorNoise(float xCoord, float yCoord)
    {
        float sample = Mathf.PerlinNoise(xCoord + redSeed, yCoord + redSeed);
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
                float redSample = ColorNoise(xCoord + redSeed, yCoord + redSeed);
                float greenSample = ColorNoise(xCoord + greenSeed, yCoord + greenSeed);
                float blueSample = ColorNoise(xCoord + blueSeed, yCoord + blueSeed);
                Color color = new Color(redSample, greenSample, blueSample);
                texture.SetPixel(x, y, color);
            }
        }
        texture.Apply();
    }
}
