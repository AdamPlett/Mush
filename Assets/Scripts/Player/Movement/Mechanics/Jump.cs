using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Jump : MonoBehaviour
{
    [SerializeField] private StatsMovement stats;


    private Controller controller;
    private Rigidbody2D rb;
    private Ground ground;
    private Vector2 velocity;
    private SpriteSelector select;

    public int jumpPhase;
    private float defaultGravityScale, jumpSpeed, doubleJumpMaxSpeed;

    private bool desiredJump, grounded, wasInAir=false, canJumpAgain=true;

    private float jumpInputDelayTimer = 0f, jumpBufferTimer = 0f, coyoteTimeTimer = 0f;
    private float timeBetweenJumpInputs = -1f;

    private AudioSource SFXPlayer;
    public AudioClip jumpSFX;
    public AudioClip doubleJumpSFX;

    private void Awake()
    {
        controller = GetComponent<Controller>();
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        SFXPlayer = GetComponent<AudioSource>();
        defaultGravityScale = 1f;
        select = GetComponent<SpriteSelector>();
    }

    private void Update()
    {
        jumpInputDelayTimer -= Time.deltaTime;
        if (jumpInputDelayTimer < 0)
        {

            desiredJump |= controller.input.RetrieveJumpInput();
            if (controller.input.RetrieveJumpInput()==false)
            {
                canJumpAgain = true;
            }

            if (desiredJump)
            {
                jumpInputDelayTimer = stats.jumpInputDelay;
                jumpBufferTimer = stats.jumpBuffer;
                jumpSpeed = Mathf.Sqrt(-2f * Physics2D.gravity.y * stats.jumpHeight);
                doubleJumpMaxSpeed = jumpSpeed * stats.maxDoubleJumpSpeed;
            }
        }
    }

    private void FixedUpdate()
    {
        jumpBufferTimer -= Time.deltaTime;
        coyoteTimeTimer -= Time.deltaTime;
        timeBetweenJumpInputs -= Time.deltaTime;

        grounded = ground.grounded;
        velocity = rb.velocity;

        //if player is grounded than reset double jumps and coyote timer
        if (grounded)
        {
            jumpPhase = 0;
            coyoteTimeTimer = stats.coyoteTime;

            //Player is landing so start landing animation
            if (wasInAir)
            {
                select.LandSequence();
            }
        }

        if (jumpBufferTimer > 0)
        { 
            desiredJump = false;
            JumpAction();
        }

        if (rb.velocity.y < 0) rb.gravityScale = stats.downwardsGravityMultiplier;
        else if (rb.velocity.y == 0) rb.gravityScale = defaultGravityScale;
        else if (rb.velocity.y > 0 && controller.input.RetrieveJumpInput()) rb.gravityScale = stats.upwardsGravityMultiplier;
        else rb.gravityScale = stats.downwardsGravityMultiplier;

        if (velocity.y < stats.maxFallSpeed )
        {
            velocity.y = stats.maxFallSpeed;
        }

        rb.velocity = velocity;
        wasInAir = !grounded;
    }

    private void JumpAction()
    {
        if (jumpBufferTimer > 0 && coyoteTimeTimer > 0 && timeBetweenJumpInputs < 0 && canJumpAgain==true)
        {
            timeBetweenJumpInputs = stats.jumpInputDelay;
            SFXPlayer.PlayOneShot(jumpSFX);
            
            jumpPhase += 1;

            //don't add more speed if you're already at max jump speed
            if (velocity.y > 0) jumpSpeed = Mathf.Max(jumpSpeed - velocity.y, 0f);
            //else if (velocity.y < 0) jumpSpeed = Mathf.Abs(rb.velocity.y);

            velocity.y += jumpSpeed;
            Debug.Log("Jump Applied");
            //Player has to let go off jump before they can jump again
            canJumpAgain = false;
            select.JumpSequence();
        }
        else if (jumpPhase < stats.doubleJumps && timeBetweenJumpInputs < 0 && canJumpAgain == true)
        {
            timeBetweenJumpInputs = stats.jumpInputDelay;
            SFXPlayer.PlayOneShot(doubleJumpSFX);

            jumpPhase += 1;

            //don't add more speed if you're already at max jump speed
            if (velocity.y > 0) doubleJumpMaxSpeed = Mathf.Max(doubleJumpMaxSpeed - velocity.y, 0f);
            
            else if (velocity.y < 0) doubleJumpMaxSpeed += -velocity.y;

            velocity.y += doubleJumpMaxSpeed;
            Debug.Log("Jump Applied");
            //Player has to let go of jump before they can jump again
            canJumpAgain = false;
            select.JumpSequence();
        }
    }
}

