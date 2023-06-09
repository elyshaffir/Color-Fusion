using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float _speed = 0.02f;
    public float range;
    public EnvironmentSpawner environmentSpawner;

    private float _checkRange = 0;
    private bool _isDead = false;
    private int _colorIndex;
    private SpriteRenderer _sprite;
    [SerializeField] private GameObject _leftoverPrefab;
    private Leftover _leftover;

    private float _leftPositionX;
    private float _rightPositionX;


    public void Init(Vector3 startPosition, float leftPositionX, float rightPositionX)
    {
        transform.position = startPosition;
        _leftPositionX = leftPositionX;
        _rightPositionX = rightPositionX;
    }

    void Start()
    {
        _sprite = GetComponent<SpriteRenderer>();

        if (Random.value >= 0.5)
        {
            _speed *= -1;
        }

        _colorIndex = Random.Range(0, 3) * 2;
        _sprite.color = Colors.colors[_colorIndex];

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

                ColorsEnvironment environment;
                if (environmentSpawner.TryGetEnvironmentByLocation(transform.position, out environment))
                {
                    Color touchingColor = environment.GetColorInLocation(transform.position);
                    int touchingColorIndex;
                    if (Colors.TryGetIndexOfColor(touchingColor, out touchingColorIndex))
                    {
                        if (_colorIndex == touchingColorIndex)
                        {
                            _leftover.color = _colorIndex;
                        }
                        else
                        {
                            // todo convert to color blending with colors, not indices
                            _leftover.color = (int)((_colorIndex + touchingColorIndex) / 2 + (Mathf.Abs(_colorIndex - touchingColorIndex) - 2) * 1.5);
                        }
                    }
                    else
                    {
                        Debug.Log("This shouldn't happen (color to index enemy)"); // todo proper handling
                    }
                }
                else
                {
                    Debug.Log("This shouldn't happen (location to environment enemy)"); // todo proper handling
                }

                // find what color should the splash be 
                leftover.transform.localScale = new Vector3(3, 3, transform.localScale.z);
            }

            Destroy(gameObject);
        }
    }

    void FixedUpdate()
    {
        if (transform.position.x <= _leftPositionX + 0.2f || transform.position.x >= _rightPositionX - 0.2f)
        {
            _speed *= -1;
        }

        transform.position += new Vector3(_speed, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            _isDead = true;
        }
    }
}
