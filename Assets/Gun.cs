using UnityEngine;

public class Gun : MonoBehaviour
{
    private Vector2 _mousePos;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private Transform _firingPoint;
    [Range(0.1f, 2f)]
    [SerializeField] private float _fireRate = 0.5f;
    private float _fireTimer;


    void Update()
    {
        _mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        float angle = Mathf.Atan2(_mousePos.y - transform.position.y, _mousePos.x - transform.position.x) * Mathf.Rad2Deg - 90f;

        transform.localRotation = Quaternion.Euler(0, 0, angle);

        if (Input.GetMouseButton(0) && _fireTimer <= 0f)
        {
            Shoot();
            _fireTimer = _fireRate;
        }
        else {
            _fireTimer -= Time.deltaTime;
        }
    }

    private void Shoot()
    {
        Instantiate(_bulletPrefab, _firingPoint.position, _firingPoint.rotation);
    }
}
