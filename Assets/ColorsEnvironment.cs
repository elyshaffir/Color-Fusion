using UnityEngine;

// todo this class looks like a stub, is it useful? maybe move some pixel logic from main guy to here.
// pixel logic = what color does the player touch?
public class ColorsEnvironment : MonoBehaviour
{
    public int width;
    public int height;
    public Texture2D texture;

    public Color GetColorInLocation(Vector3 location)
    {
        Vector2 textureCoords = new Vector2(
                    location.x - transform.position.x + (transform.localScale.x / 2),
                    location.y - transform.position.y + (transform.localScale.y) / 2);

        textureCoords *= new Vector2(
            width / transform.localScale.x,
            height / transform.localScale.y);

        return texture.GetPixel((int)textureCoords.x, (int)textureCoords.y);
    }
}
