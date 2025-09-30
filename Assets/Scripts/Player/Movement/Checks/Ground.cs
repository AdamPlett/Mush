using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    public bool grounded { get; private set; }
    public float friction { get; private set; }
    public Vector2 lastGroundedPos { get; private set; }
    private Vector2 groundPosSafeNet;
    private Vector2 normal;
    private PhysicsMaterial2D material;
    private Collision2D lastCollision;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        lastCollision = collision;
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        EvaluateCollision(collision);
        RetrieveFriction(collision);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        grounded = false;
        friction = 0;
        lastCollision = null;
    }

    private void EvaluateCollision(Collision2D collision)
    {
        for (int i = 0; i < collision.contactCount; i++)
        {
            normal = collision.GetContact(i).normal;
            grounded |= normal.y >= 0.9f;

            if (lastCollision!=null)
            {
                if (grounded && lastCollision.gameObject.tag != "Spikes")
                {
                    groundPosSafeNet = gameObject.transform.position;
                    StartCoroutine(CheckGroundPos(groundPosSafeNet));
                }
            }
        }
    }

    private void RetrieveFriction(Collision2D collision)
    {
        material = collision.rigidbody.sharedMaterial;

        friction = 0;

        if (material != null)
        {
            friction = material.friction;
        }
    }
    IEnumerator CheckGroundPos(Vector2 pos)
    {
        yield return new WaitForSeconds(.1f);
        if (grounded)
        {
            lastGroundedPos = pos;
        }
    }
}
