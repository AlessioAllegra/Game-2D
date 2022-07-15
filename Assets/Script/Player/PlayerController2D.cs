using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController2D : MonoBehaviour
{
    private Rigidbody2D rb;

    //Movement Player
    [SerializeField] float movementSpeed = 7.0f;
    [SerializeField] float turnTimerSet = 0.1f;
    private string AxisControlX;
    private string AxisControlY;
    private float movementInputDirection;
    private float movementInputVertical;
    private float turnTimer;
    private bool isFacingRight = true;
    private bool isRunning;
    private bool canMove;
    private bool canFlip;
    private int facingDirection = 1;


    //Jump Player
    [SerializeField] float JumpForce = 6.0f;
    [SerializeField] float groundCheckRadius;
    [SerializeField] float movementForceInAir;
    [SerializeField] float airDragMultiplier = 0.95f;
    [SerializeField] float variableJumpHeightMultiplier = 0.5f;
    [SerializeField] Transform feetPos;
    [SerializeField] LayerMask whatIsGround;
    private bool isGrounded;
    private bool canJump;
    //Multy Jump
    [SerializeField] int amountOfJumps = 1;
    private int amountOfJumpsLeft;

    //Wall
    [SerializeField] float wallCheckDistance;
    [SerializeField] float wallSlideSpeed;
    [SerializeField] Transform wallCheck;
    [SerializeField] LayerMask whatIsWall;
    private bool isTouchingWall;
    private bool isWallSliding;

    //Dash
    [SerializeField] float dashTime;
    [SerializeField] float dashSpeed;
    [SerializeField] float distanceBetweenImages;
    [SerializeField] float dashCoolDown;
    private float dashTimeLeft;
    private bool isDashing;
    private float lastImageXpos;
    private float lastDash;

    //Stair
    [SerializeField] Transform pointA;
    [SerializeField] Transform pointB;
    [SerializeField] LayerMask whatIsStair;
    private bool isClimbing;
    private bool isStair;

    //AllAnimation
    private Animator anim;

    private void Awake()
    {
        bool normalControl = SaveGame.GetUseArrows();
        AxisControlX = normalControl ? "Horizontal1" : "Horizontal2";
        AxisControlY = normalControl ? "Vertical1" : "Vertical2";
    }

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        amountOfJumpsLeft = amountOfJumps;
    }

    private void Update()
    {
        CheckInput();
        CheckMovementDirection();
        ChangeAxisControlInGame();
        UpdateAnimations();
        CheckIfCanJump();
        CheckIfWallSliding();
        CheckDash();
        CheckStair();        
    }

    private void FixedUpdate()
    {
        ApplyMovement();
        CheckGroundAndWall();
    }

    //Method Check input movement and jump
    private void CheckInput()
    {
        movementInputDirection = Input.GetAxisRaw(AxisControlX);
        movementInputVertical = Input.GetAxisRaw(AxisControlY);


        if (Input.GetButtonDown("Jump"))
        {
            Jump();
        }

        if(Input.GetButtonUp("Jump"))
        {
            rb.velocity = new Vector2(rb.velocity.x, rb.velocity.y * variableJumpHeightMultiplier);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyDown(KeyCode.RightShift) || Input.GetKeyDown(KeyCode.C))
        {            
            if (Time.time >= (lastDash + dashCoolDown))
            {                
                AttemptToDash();
            }                
        }

        if (Input.GetButtonDown(AxisControlX) && isTouchingWall)
        {
            if (!isGrounded && movementInputDirection != facingDirection)
            {
                canMove = false;
                canFlip = false;

                turnTimer = turnTimerSet;
            }
        }

        if (turnTimer >= 0)
        {
            turnTimer -= Time.deltaTime;

            if (turnTimer <= 0)
            {
                canMove = true;
                canFlip = true;
            }
        }

    }

    //Methods Movement
    private void ChangeAxisControl()
    {
        AxisControlX = AxisControlX.Equals("Horizontal2") ? "Horizontal1" : "Horizontal2";
        AxisControlY = AxisControlY.Equals("Vertical2") ? "Vertical1" : "Vertical2";
    }

    private void ChangeAxisControlInGame()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            ChangeAxisControl();
            Debug.Log("Change Control = " + AxisControlX + " " + AxisControlY);
        }        
    }

    private void CheckMovementDirection()
    {
        if(isFacingRight && movementInputDirection < 0)
        {
            Flip();
        }
        else if(!isFacingRight && movementInputDirection > 0)
        {
            Flip();
        }

        if(Mathf.Abs(rb.velocity.x) >= 0.01f)
        {
            isRunning = true;
        }
        else
        {
            isRunning = false;
        }
    }    

    private void ApplyMovement()
    {
        if (!isGrounded && !isWallSliding && movementInputDirection == 0)
        {
            rb.velocity = new Vector2(rb.velocity.x * airDragMultiplier, rb.velocity.y);
        }
        else if (canMove && !isWallSliding)
        {
            rb.velocity = new Vector2(movementSpeed * movementInputDirection, rb.velocity.y);
        }

        if (isWallSliding)
        {
            if(rb.velocity.y < -wallSlideSpeed)
            {
                rb.velocity = new Vector2(rb.velocity.x, -wallSlideSpeed);
            }
        }

        if(isClimbing)
        {
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(rb.velocity.x, movementInputVertical * (movementSpeed -1));
        }
        else
        {
            rb.gravityScale = 5f;
        }
    }

    private void Flip()
    {
        if(!isWallSliding && canFlip)
        {
            facingDirection *= -1;
            isFacingRight = !isFacingRight;
            transform.Rotate(0.0f, 180.0f, 0.0f);
        }        
    }

    private void DisableFlip()
    {
        canFlip = false;
    }

    private void EnableFlip()
    {
        canFlip = true;
    }
        
    //Methods Jump
    private void Jump()
    {
        if(canJump)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
            amountOfJumpsLeft--;
            SoundManager.Instance.PlayRandomPlayerJumpSound();
        }
        
    }

    private void CheckIfCanJump()
    {
        if (isGrounded && rb.velocity.y <= 0)
        {
            amountOfJumpsLeft = amountOfJumps;
        }

        if (amountOfJumpsLeft <= 0)
        {
            canJump = false;
        }
        else
        {
            canJump = true;
        }
    }    

    private void CheckGroundAndWall()
    {
        isGrounded = Physics2D.OverlapCircle(feetPos.position, groundCheckRadius, whatIsGround);
        isTouchingWall = Physics2D.Raycast(wallCheck.position, transform.right, wallCheckDistance, whatIsWall);
        isStair = Physics2D.OverlapArea(pointA.position, pointB.position, whatIsStair);
    }

    //Methods Wall
    private void CheckIfWallSliding()
    {
        if(isTouchingWall && !isGrounded && rb.velocity.y < 0)
        {
            isWallSliding = true;
        }
        else
        {
            isWallSliding = false;
        }
    }

    //Methods Dash
    private void AttemptToDash()
    {
        SoundManager.Instance.PlayRandomPlayerDashSound();
        isDashing = true;
        dashTimeLeft = dashTime;
        lastDash = Time.time;

        PlayerAfterImagePool.Instance.GetFromPool();
        lastImageXpos = transform.position.x;
    }

    private void CheckDash()
    {
        if (isDashing)
        {            
            if (dashTimeLeft > 0)
            {                
                canMove = false;
                canFlip = false;                
                rb.velocity = new Vector2(dashSpeed * facingDirection, rb.velocity.y);
                dashTimeLeft -= Time.deltaTime;

                if (Mathf.Abs(transform.position.x - lastImageXpos) > distanceBetweenImages)
                {
                    PlayerAfterImagePool.Instance.GetFromPool();
                    lastImageXpos = transform.position.x;
                }
            }

            if (dashTimeLeft <= 0 || isTouchingWall)
            {
                isDashing = false;
                canMove = true;
                canFlip = true;
            }

        }
    }

    //Methods Stair
    private void CheckStair()
    {
        if(isStair && Mathf.Abs(movementInputVertical) > 0f)
        {
            isClimbing = true;            
        }
        else if(!isStair)
        {
            isClimbing = false;            
        }
    }

    //Methods Animation
    private void UpdateAnimations()
    {
        anim.SetBool("isRunning", isRunning);
        anim.SetBool("isGrounded", isGrounded);
        anim.SetFloat("yVelocity", rb.velocity.y);
        anim.SetBool("isWallSliding", isWallSliding);
        anim.SetBool("isClimbing", isClimbing);
    }

    //PowerUp speed
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 22)
        {
            Debug.Log("PowerUp Speed");
            collision.gameObject.SetActive(false);
            StartCoroutine("BoostSpeed");
        }
    }

    IEnumerator BoostSpeed()
    {
        Debug.Log("boost speed");
        movementSpeed = 7f;
        yield return new WaitForSeconds(10f);
        movementSpeed = 3.5f;
    }

    //Gizmos
    private void OnDrawGizmos()
    {
        Gizmos.DrawWireSphere(feetPos.position, groundCheckRadius);

        Gizmos.DrawLine(wallCheck.position, new Vector3(wallCheck.position.x + wallCheckDistance, wallCheck.position.y, wallCheck.position.z));
    }

}
