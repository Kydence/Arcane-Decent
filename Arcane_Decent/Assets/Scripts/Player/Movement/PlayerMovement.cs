using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Parameters")]
    public float speed;
    public float jumpPower;

    [Header("Coyote Time")]
    public float coyoteTime; // how much time the player can hang in the air before jumping
    float coyoteCounter; // how much time passed since the player ran off the edge

    [Header("Multiple Jumps")]
    public int extraJumps;
    int jumpCounter;
    
    [Header("Layers")]
    public LayerMask groundLayer;
    public LayerMask wallLayer;
    
    /*
    [Header("Sounds")]
    public AudioClip jumpSound;
    */

    public Rigidbody2D body;
    Animator anim;
    BoxCollider2D boxCollider;
    float wallJumpCooldown;
    float horizontalInput;

    void Awake()
    {
        // grab references from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");

        // flip player when moving left
        if (horizontalInput > 0.01f)
        {
            transform.localScale = new Vector3(2, 2, 2);
        }
        else if (horizontalInput < -0.01f)
        {
            transform.localScale = new Vector3(-2, 2, 2);
        }

        // set animator parameters
        anim.SetBool("IsWalk", horizontalInput != 0);
        //anim.SetBool("IsJump", isGrounded());

        // jump
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }

        // adjustable jump height
        if (Input.GetKeyUp(KeyCode.Space) && body.linearVelocity.y > 0)
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, body.linearVelocity.y / 2);
        }

        if (onWall())
        {
            body.gravityScale = 0;
            body.linearVelocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 7;
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            if (isGrounded())
            {
                coyoteCounter = coyoteTime; // reset coyote counter when on ground
                jumpCounter = extraJumps; // reset jump counter to extra jump value
            }
            else
            {
                coyoteCounter -= Time.deltaTime; // start decreasing coyote counter when not on ground
            }
        }
    }

    private void Jump()
    {
        if (coyoteCounter < 0 && !onWall() && jumpCounter <= 0)
        {
            return; // if coyote counter is 0 or less and not on wall and don't have extra jumps don't do anything
        }

        //SoundManager.instance.PlaySound(jumpSound);

        if (onWall())
        {
            WallJump();
        }
        else
        {
            if (isGrounded())
            {
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            }
            else
            {
                // if not on the ground and coyote counter is bigger than 0 do a normal jump
                if (coyoteCounter > 0)
                {
                    body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                }
                else
                {
                    if (jumpCounter > 0) // if there are extra jumps then jump and decrease the jump counter
                    {
                        body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                        jumpCounter--;
                    }
                }
            }

            // reset coyote counter to 0 to avoid double jumps
            coyoteCounter = 0;
        }

        /*
        if (isGrounded())
        {
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
        }
        else if (onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
            {
                body.linearVelocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            
            wallJumpCooldown = 0;
        }*/
    }
    
    private void WallJump()
    {
        
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        
    }*/

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }






    /*
    // wall jump logic
        if (wallJumpCooldown > 0.2f)
        {
            body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.linearVelocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 1;
            }

            if (Input.GetKey(KeyCode.Space))
            {
                Jump();

                if (Input.GetKeyDown(KeyCode.Space) && isGrounded())
                {
                    SoundManager.instance.PlaySound(jumpSound);
                }
            }
        }
        else
        {
            wallJumpCooldown += Time.deltaTime;
        }*/
}
