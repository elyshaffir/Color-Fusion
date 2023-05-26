using UnityEngine;

public class MainGuy : MonoBehaviour
{
    public float jumpSpeed = 600;
    public float speed = 5;
    public float viewHeight = 10; // todo can this be calculated

    private bool isJumping;
    private Rigidbody2D myRidigbody;

    void Start()
    {
        myRidigbody = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float move = Input.GetAxis("Horizontal");

        myRidigbody.velocity = new Vector2(speed * move, myRidigbody.velocity.y);

        if (Input.GetButtonDown("Jump") && !isJumping)
        {
            myRidigbody.AddForce(new Vector2(myRidigbody.velocity.x, jumpSpeed));
        }

        // check if dead
        if (myRidigbody.transform.position.y + viewHeight < Camera.main.transform.position.y)
        {
            Debug.Log("out"); // todo handle death
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.CompareTag("Ground"))
        {
            isJumping = false;
        }
    }

    private void OnCollisionExit2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            isJumping = true;
        }
    }
}
