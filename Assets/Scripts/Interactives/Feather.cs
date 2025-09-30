using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Feather : MonoBehaviour
{
    private SpriteRenderer sr;
    private PolygonCollider2D poly;
    private bool featherPop = false;
    [Tooltip("How long before the feather respawns"), Min(.5f)]
    public float duration = 2.5f;
    private void Awake()
    {
        sr = GetComponent<SpriteRenderer>();
        poly = GetComponent<PolygonCollider2D>();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (featherPop) return;
            GameObject player = collision.gameObject;
            Dash dash = player.GetComponent<Dash>();
            dash.dashPhase -=1;
            featherPop = true;

            sr.enabled = false;
            poly.enabled = false;
            Invoke("RespawnFeather", duration);
        }
    }
    private void RespawnFeather()
    {
        sr.enabled = true;
        poly.enabled = true;
        featherPop = false;
    }
}
