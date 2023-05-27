using UnityEngine;
using System;

public class MainGuy : MonoBehaviour
{
    public float jumpSpeed = 600;
    public float speed = 5;
    public float speedEffect = 1;
    public float bounceBoost = 8;
    public float viewHeight; // todo can this be calculated
    public LogicScript logic;
    public EnvironmentSpawner environmentSpawner;
    public float effectTime = 5;

    private bool _isJumping;
    private bool _isDead = false;
    private Rigidbody2D _myRidigbody;
    private int _invertControl;
    private float _confuseTimer = 0;
    private float _speedBoostTimer = 0;
    private float _bounceTimer = 0;

    private Vector2 _screenBounds;

    void Start()
    {
        _invertControl = 1;
        _myRidigbody = GetComponent<Rigidbody2D>();
        viewHeight = Camera.main.orthographicSize;
        logic = GameObject.FindGameObjectWithTag("Logic").GetComponent<LogicScript>();
        _screenBounds = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height, Camera.main.transform.position.z));
    }

    void Update()
    {
        if (_bounceTimer > 0)
        {
            _bounceTimer -= Time.deltaTime;
        }
        else
        {
            float move = Input.GetAxis("Horizontal") * _invertControl * speedEffect;
            _myRidigbody.velocity = new Vector2(speed * move, _myRidigbody.velocity.y);
        }

        if (Input.GetButtonDown("Jump") && !_isJumping)
        {
            _myRidigbody.AddForce(new Vector2(_myRidigbody.velocity.x, jumpSpeed * speedEffect));
        }

        // check if dead or our of view
        if (_isDead || LogicScript.isOutOfScreen(transform.position.y))
        {
            logic.gameOver();
            Destroy(gameObject);
            Debug.Log("Game Over");
        }

        ColorsEnvironment environment;
        if (environmentSpawner.TryGetEnvironmentByLocation(transform.position, out environment))
        {
            Color affectingColor = environment.GetColorInLocation(transform.position);
            ColorEffect effect;
            if (ColorEffectMapping.TryGetValue(affectingColor, out effect))
            {
                switch (effect)
                {
                    case ColorEffect.Heal:
                        Debug.Log("Healing");
                        break;
                    default:
                        throw new NotImplementedException("ColorEffect not implemented!");
                }
            }
            else
            {
                Debug.Log("This shouldn't happen (color to effect main guy)"); // todo proper handling
            }
        }
        else
        {
            Debug.Log("This shouldn't happen (environment to location main guy)"); // todo proper handling
        }

        // effect timers
        if (_confuseTimer > 0)
        {
            _confuseTimer -= Time.deltaTime;
        }
        else
        {
            _invertControl = 1;
        }


        if (_speedBoostTimer > 0)
        {
            _speedBoostTimer -= Time.deltaTime;
        }
        else
        {
            speedEffect = 1;
        }
    }

    private void LateUpdate()
    {
        Vector3 viewPos = transform.position;
        viewPos.x = Mathf.Clamp(viewPos.x, _screenBounds.x * (-1), _screenBounds.x);
        transform.position = viewPos;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isJumping = false;
        }

        // stop game when killed by an enemy
        if (other.gameObject.CompareTag("Enemy"))
        {
            _isDead = true;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isJumping = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Effect");
        if (other.gameObject.CompareTag("Leftover"))
        {
            Leftover leftover = other.gameObject.GetComponent<Leftover>();
            switch (leftover.color)
            {
                case 1:
                    float xVelocity = transform.position.x - leftover.transform.position.x;
                    float yVelocity = transform.position.y - leftover.transform.position.y;
                    _myRidigbody.velocity = new Vector2(xVelocity * bounceBoost, yVelocity * bounceBoost);
                    _invertControl = 0;
                    _bounceTimer = 0.5f;
                    break;

                case 3:
                    speedEffect = 3;
                    _speedBoostTimer = effectTime;
                    break;

                case 5:
                    _invertControl = -1;
                    _confuseTimer = effectTime;
                    break;
            }
        }
    }
}
