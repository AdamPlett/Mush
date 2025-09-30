using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeBall : MonoBehaviour
{
    private GameObject player;
    private Ground ground;
    private SpriteSelector select;
    private bool collided = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            player = collision.gameObject;
            select = player.GetComponent<SpriteSelector>();
            collided = select.isHurt;
            if (!collided)
            {
                ground = player.GetComponent<Ground>();
                select.HurtSequence();
                Invoke(nameof(RespawnPlayer), .15f);
            }
        }
    }
    private void RespawnPlayer()
    {
        player.transform.SetPositionAndRotation(ground.lastGroundedPos + new Vector2(0f, .5f), player.transform.rotation);
    }
    private void ResetCollider()
    {
        collided = false;
    }
}