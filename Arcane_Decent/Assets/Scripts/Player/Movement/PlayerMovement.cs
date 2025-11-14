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
    int jumpsUsedSinceGrounded;
    bool wasGrounded;
    
    [Header("Layers")]
    public LayerMask groundLayer;
    
    [Header("Sounds")]
    public AudioClip jumpSound;
    public AudioClip walkSound;

    public Rigidbody2D body;
    Animator anim;
    BoxCollider2D boxCollider;
    float horizontalInput;

    void Awake()
    {
        // grab references from object
        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    void Start()
    {
        if (PowerupState.instance != null && PowerupState.instance.hasDoubleJump)
        {
            EnableDoubleJump();
        }
        else
        {
            // no double jummp at start
            extraJumps = 0;
            jumpCounter = 0;
        }
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

        // check grounded once per frame
        bool grounded = isGrounded();

        // set animator parameters
        anim.SetBool("IsWalk", horizontalInput != 0);
        anim.SetBool("Isgrounded", isGrounded());
        
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

        body.gravityScale = 7;
        body.linearVelocity = new Vector2(horizontalInput * speed, body.linearVelocity.y);

        if (grounded)
        {
            coyoteCounter = coyoteTime; // reset coyote counter when on ground
            jumpCounter = extraJumps; // reset jump counter to extra jump value

            // only reset jumps used when actually landing on ground
            if (!wasGrounded)
            {
                jumpsUsedSinceGrounded = 0;
            }
        }
        else
        {
            coyoteCounter -= Time.deltaTime; // start decreasing coyote counter when not on ground
        }
        
        isFalling();
    }

    public void PlayFootsteps()
    {
        if (isGrounded() && Mathf.Abs(horizontalInput) > 0.01f)
        {
                CloseSoundManager.instance.PlaySound(walkSound);
        }
    }

    private void Jump()
    {
        int maxJumps = 1 + extraJumps;

        // already used all jumps since landed on ground
        if (jumpsUsedSinceGrounded >= maxJumps)
        {
            return;
        }
        
        // if you can't jump, bail
        if (coyoteCounter < 0 && jumpCounter <= 0)
        {
            return; // if coyote counter is 0 or less and not on wall and don't have extra jumps don't do anything
        }

        CloseSoundManager.instance.PlaySound(jumpSound);

        if (isGrounded())
        {
            // ground jump
            body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
            jumpsUsedSinceGrounded++;
        }
        else
        {
            // air jump
            // if not on the ground and coyote counter is bigger than 0 do a normal jump
            if (coyoteCounter > 0)
            {
                // coyote jump consumes jump and not counted as extra jump
                body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                jumpsUsedSinceGrounded++;
            }
            else
            {
                if (jumpCounter > 0) // extra jumps from powerup
                {
                    body.linearVelocity = new Vector2(body.linearVelocity.x, jumpPower);
                    jumpCounter--;
                    jumpsUsedSinceGrounded++;
                }
            }
        }

        // reset coyote counter to 0 to avoid double jumps
        coyoteCounter = 0;
    }

    /*void OnCollisionEnter2D(Collision2D collision)
    {
        
    }*/

    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.05f, groundLayer);
        return raycastHit.collider != null;
    }

    public void EnableDoubleJump()
    {
        extraJumps = 1;
        jumpCounter = extraJumps;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded();
    }

    private void isFalling()
    {
        if (body.linearVelocity.y > 0)
        {
            anim.SetBool("IsJump", true);
            anim.SetBool("IsFalling", false);
        }
        if (body.linearVelocity.y < 0)
        {
          
            anim.SetBool("IsJump", false);
            anim.SetBool("IsFalling", true);

            
        }
        else
        {
            anim.SetBool("IsFalling", false);
        }
    }
}