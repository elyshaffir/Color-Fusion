using UnityEngine;

public class Leftover : MonoBehaviour
{
    public int color = 0;

    private SpriteRenderer _sprite;

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.color = Colors.colors[color];
    }
}
