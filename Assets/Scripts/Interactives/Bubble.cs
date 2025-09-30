using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bubble : MonoBehaviour
{
    private SpriteRenderer sr;
    private CircleCollider2D circle;
    private bool bubblePop = false;
    [Tooltip("How long before the bubble respawns"), Min(1)]
    public float duration=5;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        circle = GetComponent<CircleCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //player has two colliders and this stops them from getting two extra double jumps if both colliders hit at the same time
            if (bubblePop) return;

            GameObject player = collision.gameObject;
            Jump jump = player.GetComponent<Jump>();
            jump.jumpPhase -= 1;
            bubblePop = true;
            sr.enabled = false;
            circle.enabled = false;
            Invoke("RespawnBubble", duration);
        }
    }
    private void RespawnBubble()
    {
        sr.enabled = true;
        circle.enabled = true;
        bubblePop = false;
    }
}
