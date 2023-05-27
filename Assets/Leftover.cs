using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leftover : MonoBehaviour
{
    // 0 = red
    // 1 = orange
    // 2 = yellow
    // 3 = green
    // 4 = blue
    // 5 = purple
    public int color = 0;

    private SpriteRenderer _sprite;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();
        _sprite.color = LogicScript.returnColor(color);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
