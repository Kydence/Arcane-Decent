using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed;
    public Rigidbody2D body;
    Animator anim;
    bool grounded;

    void Awake()
    {
        // grab references for rigidbody and animator fromm object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        // flip player when moving left
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(2, 2, 2);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }

        // jump
        if (Input.GetKey(KeyCode.Space) && grounded)
        {
            Jump();
        }

        // set animator parameters
        anim.SetBool("IsWalk", horizontalInput != 0);
        // anim.SetTrigger("IsFalling");
        // anim.SetBool("IsJump", grounded);
    }

    private void Jump()
    {
        body.linearVelocity = new Vector2(body.linearVelocity.x, speed);
        grounded = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            grounded = true;
        }
    }
}
