using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HornlerMovement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2f;
    [SerializeField] float stopDistance = 5f;
    [SerializeField] float shootDistance = 5f;
    [SerializeField] GameObject bulletPrefab;
    [SerializeField] Transform bulletSpawnPoint;
    [SerializeField] float bulletSpeed = 5f;
    [SerializeField] float shootDelay = 3f;
    public int health = 5;


    Transform playerTransform;
    Rigidbody2D rb;
    Animator myAnimator;
    SpriteRenderer spriteRenderer;

    bool isWalking = false;
    bool isShooting = false;

    void Start()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
        myAnimator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);

        if (distanceToPlayer > stopDistance)
        {
            // Move towards player
            Vector2 direction = (playerTransform.position - transform.position).normalized;
            rb.velocity = direction * moveSpeed;
            isWalking = true;

            // Flip sprite if necessary
            if (direction.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            // Stop moving
            rb.velocity = Vector2.zero;
            isWalking = false;
        }

        if (distanceToPlayer <= stopDistance && distanceToPlayer > shootDistance)
        {
            // code for stopping and shooting
        }
        else if (distanceToPlayer <= shootDistance && !isShooting)
        {
            // code for shooting
            StartCoroutine(Shoot());
        }

        // Update animator parameters
        myAnimator.SetBool("isWalking", isWalking);
        if (isShooting)
        {
            myAnimator.SetTrigger("Shooting");
        }
        
    }

    IEnumerator Shoot()
    {
        isShooting = true;
        // Wait for delay
        yield return new WaitForSeconds(shootDelay);

        // Spawn bullet and set velocity towards player
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, Quaternion.identity);
        Vector2 direction = (playerTransform.position - bulletSpawnPoint.position).normalized;
        bullet.GetComponent<Rigidbody2D>().velocity = direction * bulletSpeed;

        // Play shooting sound effect
        AudioSource audioSource = GetComponent<AudioSource>();
        if (audioSource != null)
        {
            audioSource.Play();
        }

        // Set isShooting to false
        isShooting = false;
    }

    void OnCollisionEnter2D(Collision2D other)
{
    if(other.gameObject.CompareTag("Bullets")) 
    {
        health -= 1;

        // Check if Hornler is dead
        if (health <= 0)
        {
            Debug.Log("Hornler is dead!");
            myAnimator.SetTrigger("Death");
            StartCoroutine(Die());
        }
        Debug.Log("Hornler was hit by a bullet!");

    }
}



    IEnumerator Die()
{
    yield return new WaitForSeconds(1f);

    // Disable the game object
    gameObject.SetActive(false);
}


}
