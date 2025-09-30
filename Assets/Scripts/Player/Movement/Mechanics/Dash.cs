using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    [SerializeField] private StatsMovement stats;

    private Controller controller;
    private Vector2 direction, dashSpeed;
    private float speed;
    private Rigidbody2D rb;

    private bool desiredDash, canDash = true, hasDashed = false;
    private Vector2 lastDir;

    public int dashPhase=0;

    private float cooldownTimer = 0;

    public bool grounded;
    private Ground ground;

    private AudioSource SFXPlayer;
    public AudioClip dashSFX;
    public AudioClip doubleDashSFX;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        controller = GetComponent<Controller>();
        ground = GetComponent<Ground>();
        SFXPlayer = GetComponent<AudioSource>();
    }
    private void Update()
    {
        direction.x = controller.input.RetrieveMoveInput();
        if (direction.x != 0) lastDir.x = direction.x;

        grounded = ground.grounded;
        if (grounded)
        {
            dashPhase = 0;
        }

        if (controller.input.RetrieveDashInput() == false)
        {
            canDash = true;
        }

        desiredDash |= controller.input.RetrieveDashInput();
        if (desiredDash)
        {
            speed = ((stats.dashDistance / stats.dashDuration) - (1 / 2) * stats.AirDeceleration) * stats.dashDuration;
            if (direction.x != 0)
            {
                speed = speed * direction.x;
            }
            else
            {
                speed = speed * lastDir.x;
            }

            dashSpeed = new Vector2(speed, stats.dashHeightDisplacement);
        }

    }
    private void FixedUpdate()
    {
        if (grounded)
        {
            dashPhase = 0;
            hasDashed = false;
        }

        if (desiredDash)
        {
            desiredDash = false;
            DashAction();
        }
        cooldownTimer -= Time.deltaTime; ;
    }
    private void DashAction()
    {
        if (cooldownTimer > 0 || dashPhase>=stats.airDashes || !canDash)
        {
            return;
        }
        //sets timer until player can dash again
        if (grounded || !hasDashed)
        {
            SFXPlayer.PlayOneShot(dashSFX);
        }
        else
        {
            SFXPlayer.PlayOneShot(doubleDashSFX);
        }
        cooldownTimer = stats.dashDuration + stats.dashCooldown;
        rb.velocity = dashSpeed;

        canDash = false;
        dashPhase += 1;
        hasDashed = true;
    }
}
