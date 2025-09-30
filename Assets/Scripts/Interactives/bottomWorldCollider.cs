using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bottomWorldCollider : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            GameObject player = collision.gameObject;
            Ground ground = player.GetComponent<Ground>();
            player.transform.SetPositionAndRotation(ground.lastGroundedPos + new Vector2(0f, .5f), player.transform.rotation);
            
        }
    }
}
