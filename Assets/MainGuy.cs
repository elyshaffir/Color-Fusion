using UnityEngine;
using System;

public class MainGuy : MonoBehaviour
{
    public float jumpSpeed = 600;
    public float speed = 5;
    public float viewHeight; // todo can this be calculated

    public EnvironmentSpawner environmentSpawner;

    private bool _isJumping;
    private Rigidbody2D _myRidigbody;

    void Start()
    {
        _myRidigbody = GetComponent<Rigidbody2D>();
        viewHeight = Camera.main.orthographicSize;
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


        foreach (GameObject environmentObject in environmentSpawner.environments)
        {
            if (environmentObject.transform.position.y < transform.position.y &&
                environmentObject.transform.position.y + environmentObject.transform.localScale.y
                 > transform.position.y)
            {
                ColorsEnvironment environment = environmentObject.GetComponent<ColorsEnvironment>();
                Vector2 textureCoords = new Vector2(
                    transform.position.x - environment.transform.position.x + (environment.transform.localScale.x / 2),
                    transform.position.y - environment.transform.position.y + (environment.transform.localScale.y) / 2);

                textureCoords *= new Vector2(
                    environment.width / environment.transform.localScale.x,
                    environment.height / environment.transform.localScale.y);

                // todo make this the center of the character
                Color affectingColor = environment.texture.GetPixel(
                    (int)textureCoords.x,
                    (int)textureCoords.y);
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
                    // todo throw exception or something, in production this should not happen!
                    // this needs to be thoroughly tested before publishing.
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
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
