using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : MonoBehaviour
{
    [SerializeField] private StatsMovement stats;

    private Controller controller;
    [SerializeField] private Sprite left, right;
    private Vector2 direction, desiredVelocity, velocity;
    private float lastDirectionX =1;
    private Rigidbody2D rb;
    private Ground ground;

    private float maxSpeedChange, acceleration;
    private bool grounded;

    private SpriteSelector select;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        ground = GetComponent<Ground>();
        controller = GetComponent<Controller>();
        select = GetComponent<SpriteSelector>();
    }

    private void Update()
    {
        //retrieves the current move input and calculates the desired velocity
        direction.x = controller.input.RetrieveMoveInput();
        if(direction.x *lastDirectionX < 0)
        {
            select.SetDirection(direction.x);
        }
        float sprintMulitplier = controller.input.RetrieveSprintInput() > 0 ? stats.sprintSpeedMultiplier : 1;
        desiredVelocity = new Vector2(direction.x, 0f)*Mathf.Max(stats.maxSpeed*sprintMulitplier-ground.friction, 0f);
        if (direction.x!=0)
        {
            lastDirectionX = direction.x;
        }
    }
    private void FixedUpdate()
    {
        grounded = ground.grounded;
        velocity = rb.velocity;

        if (direction.x != 0)
        {
            //applies either ground or air accleration based on if the player is grounded
            acceleration = grounded ? stats.groundAcceleration : stats.airAcceleration;
        }
        else
        {
            acceleration = grounded ? stats.groundDeceleration : stats.AirDeceleration;
        }
        //calculates how much the player should speed up per frame based on acceleration and desired velocity
        maxSpeedChange = acceleration * Time.deltaTime;
        velocity.x = Mathf.MoveTowards(velocity.x, desiredVelocity.x, maxSpeedChange);

        //sets rigidbodies velocity
        rb.velocity = velocity;
    }
}
