using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] float bulletSpeed = 5f;
    Rigidbody2D rgbd;
    PlayerMovement player;
    float xSpeed;

    void Start()
    {
        rgbd = GetComponent<Rigidbody2D>();
        player = FindObjectOfType<PlayerMovement>();
        xSpeed = player.transform.localScale.x * bulletSpeed;
    }

    void Update()
    {
        rgbd.velocity = new Vector2(xSpeed, 0f);
        // flip bullet sprite accordingly
        if (rgbd.velocity.x > 0)
            transform.localScale = new Vector3(1f, 1f, 1f);
        else if (rgbd.velocity.x < 0)
            transform.localScale = new Vector3(-1f, 1f, 1f);

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Enemy") 
        {
            Destroy(other.gameObject); // Destroy to enemy
        }
        
        Destroy(gameObject); // Destroy the bullet
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        Destroy(gameObject);
    }

    
}

