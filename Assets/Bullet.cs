using UnityEngine;

public class Bullet : MonoBehaviour
{
    [Range(1, 10)]
    [SerializeField] private float _speed = 10f;

    [Range(1, 10)]
    [SerializeField] private float _lifeTime = 3f;

    private Rigidbody2D _rb;

    private void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        Destroy(gameObject, _lifeTime);
    }

    private void FixedUpdate()
    {
        _rb.velocity = transform.up * _speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Destroy(gameObject);
    }

}
