using UnityEngine;

public class MainGuy : MonoBehaviour
{
    public float jumpSpeed = 600;
    public float speed = 5;
    public float viewHeight = 10; // todo can this be calculated

    // Gun variables
    [SerializeField] private GameObject _bulletPrefab;
    [Range(0.1f, 1f)]
    [SerializeField] private float _fireRate = 0.5f;

    private bool _isJumping;
    private Rigidbody2D _myRidigbody;

    void Start()
    {
        _myRidigbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        _myRidigbody.velocity = new Vector2(speed * move, _myRidigbody.velocity.y);

        if (Input.GetButtonDown("Jump") && !_isJumping)
        {
            _myRidigbody.AddForce(new Vector2(_myRidigbody.velocity.x, jumpSpeed));
        }

        // check if dead
        if (_myRidigbody.transform.position.y + viewHeight < Camera.main.transform.position.y)
        {
            Debug.Log("out"); // todo handle death
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            _isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            _isJumping = true;
        }
    }
}
