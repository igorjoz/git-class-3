using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float jumpForce;
    public float doubleJumpForce;
    public float liftingForce;

    public bool jumped;
    public bool doubleJumped;

    public LayerMask whatIsFloor;

    private Rigidbody2D rb;
    private BoxCollider2D boxCollider2D;
    private float timestamp;

    // Start is called before the first frame update
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();    // get Rigidbody2D component
        boxCollider2D = GetComponent<BoxCollider2D>();  // get BoxCollider2D component
    }

    // Update is called once per frame
    void Update()
    {

        // check if 1 second (timestamp time) has passed
        if (IsGrounded() && Time.time >= timestamp)
        {
            if (jumped || doubleJumped)
            {
                jumped = false;
                doubleJumped = false;
            }

            timestamp = Time.time + 1f; // current time + 1 second
        }

        // GetMouseButtonDown != GetMouseButton
        if (Input.GetMouseButtonDown(0))
        {
            if (!jumped)
            {
                SoundManager.instance.PlayOnceJump();
                rb.velocity = (new Vector2(0f, jumpForce)); // set force upwards
                jumped = true;
            }
            else if (!doubleJumped)
            {
                SoundManager.instance.PlayOnceJump();
                rb.velocity = (new Vector2(0f, doubleJumpForce)); // set force upwards
                //rb.AddForce(new Vector2(0f, jumpForce * 40)); // alternative way: add force instead of setting it
                doubleJumped = true;
            }
        }

        if (Input.GetMouseButton(0) && rb.velocity.y <= 0)
        {
        }

        if (Input.GetMouseButton(0) && rb.velocity.y <= 0)
        {
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstacle") && !GameManager.instance.immortality.isActive)
        {
            HandlePlayerDeath();
        }

        if (Input.GetMouseButton(0) && rb.velocity.y <= 0)
        {
        }

        else if (other.CompareTag("Coin"))
        {
            GameManager.instance.HandleCoinCollection();
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("Immortality"))
        {
            GameManager.instance.HandleImmortalityCollection();
            Destroy(other.gameObject);
        }
    }

    void HandlePlayerDeath()
    {
        rb.simulated = false;
        GameManager.instance.HandleGameOver();
    }
}
