using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "StatsMovement", menuName = "ScriptableObjects/Stats/Movement")]
public class StatsMovement : ScriptableObject
{
    [Header("Movement")]
    [Tooltip("Max horizontal walking velocity"), Min(0)]
    public float maxSpeed;

    [Tooltip("determines how much faster running is to walking"), Min(0)]
    public float sprintSpeedMultiplier;

    [Tooltip("Rate at which the player accelerates on ground"), Min(0)]
    public float groundAcceleration = 50f;

    [Tooltip("Rate at which the player accelerates in air"), Min(0)]
    public float airAcceleration = 35f;

    [Tooltip("Determines how quickly the player slows down after input ends on the ground"), Min(0)]
    public float groundDeceleration;

    [Tooltip("Determines how quickly the player slows down after input ends in air"), Min(0)]
    public float AirDeceleration;

    [Space(3)]
    [Header("Jump")]
    [Tooltip("The number of jumps the player has while in air"), Min(0)]
    public int doubleJumps;

    [Tooltip("The amount of time before a player can input another jump after inputing a jump"), Min(0)]
    public float jumpInputDelay=.1f;

    [Tooltip("The est. height the player should get from jumping"), Min(0)]
    public float jumpHeight=3;

    [Tooltip("Uses a multipler of jump speed, which is caluculated from jump height aand gravity, to get the max jump speed for double jumps."), Min(1)]
    public float maxDoubleJumpSpeed;

    //change to minimum jump height
    [Tooltip("Minimum amount of time before you can start falling because of variable jump hieght"), Min(0)]
    public float minimumJumpingTime;

    [Tooltip("Terminal velocity")]
    public float maxFallSpeed;

    [Tooltip("Gravity applied to the player to help sticking to slopes"), Min(0)]
    public float groundedGravity;

    [Tooltip("Gravity multiplier when the player is moving upwards"), Min(0)]
    public float upwardsGravityMultiplier = 1.7f;

    [Tooltip("In air gravity when vertical velocity is less than zero"), Min(0)]
    public float downwardsGravityMultiplier =3f;

    [Tooltip("Gravity at the apex of the jump to create some hangtime"), Min(0)]
    public float hangTimeGravity;

    [Tooltip("The postive velocity value that hangtime gravity kicks in (until player velocity reaches the negative value)"), Min(0)]
    public float hangTimeStartingVelocity;

    [Tooltip("Amount of time jump input can be buffered for"), Range(0, .5f)]
    public float jumpBuffer = .1f;

    [Tooltip("Amount of time you can jump after leaving ground"), Range(0, .5f)]
    public float coyoteTime = .1f;

    [Tooltip("Raycast distance to detect ground and ceiling"), Min(0)]
    public float platformDetectorDistance = .05f;

    [Space(3)]
    [Header("Dash")]
    [Tooltip("Apox. distance of the dash"), Min(0)]
    public float dashDistance;

    [Tooltip("How long the dash lasts"), Min(0)]
    public float dashDuration;

    [Tooltip("Duration + cooldown = time until next dash"), Min(0)]
    public float dashCooldown;

    [Tooltip("Helps to negate gravity while dashing"), Min(0)]
    public float dashHeightDisplacement;

    [Tooltip("Number off dashes allowed before touching ground"), Min(0)]
    public float airDashes=1;
}
