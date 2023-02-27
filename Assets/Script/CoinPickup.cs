using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinPickup : MonoBehaviour
{
    [SerializeField] AudioClip coinPickupSFX;
    [SerializeField] int coinValue = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {
            // Calling the public method on the GameSession object
            FindObjectOfType<GameSession>().AddToScore(coinValue);

            // play at the camera
            AudioSource.PlayClipAtPoint(coinPickupSFX, Camera.main.transform.position,.3f); 
            Destroy(gameObject);
        }
    }
}
