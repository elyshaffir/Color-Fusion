using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    private float _outY;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _outY = Camera.main.transform.position.y - Camera.main.orthographicSize - 1;

        if (transform.position.y < _outY)
        {
            Destroy(gameObject);
        }
    }
}
