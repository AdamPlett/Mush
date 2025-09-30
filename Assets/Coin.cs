using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    private AudioSource SFXPlayer;
    public AudioClip coinSFX;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            //Add coin counter code here

            Invoke("DestroyCoin", .25f);
        }
    }
    private void DestroyCoin()
    {
        Destroy(gameObject);
        SFXPlayer.PlayOneShot(coinSFX);
    }
}
