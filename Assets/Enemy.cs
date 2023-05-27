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

    public float _speed = 0.02f;
    public float _range;

    private float _checkRange = 0;
    private bool _isDead = false;
    private int _color;
    private SpriteRenderer _sprite;
    [SerializeField] private GameObject _leftoverPrefab;
    private Leftover _leftover;

    // Start is called before the first frame update
    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();

        if (Random.value >= 0.5)
        {
            _speed *= -1;
        }

        _color = Random.Range(0, 3);
        _sprite.color = LogicScript.returnColor(_color * 2);

    }

    void Update()
    {
        // destroy when hit by a bullet ot out of view
        if (_isDead || LogicScript.isOutOfScreen(transform.position.y))
        {
            // when shot
            if (_isDead)
            {
                // create a splash and find its script
                GameObject leftover = Instantiate(_leftoverPrefab, transform.position, Quaternion.identity);
                _leftover = leftover.GetComponent<Leftover>();

                // find what color should the splash be 
                _leftover.color = _color * 2;
                leftover.transform.localScale = new Vector3(3, 3, transform.localScale.z);
            }

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

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _isDead = true;
        }
    }
}
