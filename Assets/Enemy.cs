using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // 0 = red
    // 1 = orange
    // 2 = yellow
    // 3 = green
    // 4 = blue
    // 5 = purple
    public int _color = 0;

    public float _speed = 0.02f;
    public float _range;

    private float _checkRange = 0;
    private bool _isDead = false;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.value >= 0.5)
        {
            _speed *= -1;
        }
    }

    void Update()
    {
        // destroy when hit by a bullet ot out of view
        if (_isDead || LogicScript.isOutOfScreen(transform.position.y))
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // change diraction
        if (_checkRange > _range)
        {
            _speed *= -1;
            _checkRange = 0;
        }

        transform.position += new Vector3(_speed, 0, 0);
        _checkRange += Mathf.Abs(_speed);
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _isDead = true;
        }
    }
}
