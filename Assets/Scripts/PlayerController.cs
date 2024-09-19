using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 12000;
    public float xRange = 8;
    public float yRange = 0.54f;
    public float turnSpeed = 12000;
    private int lives = 3;
    private float powerupTimer = 0;
    private Rigidbody playerRb;
    private readonly float gravityModifier = 1;
    public bool isOnGround = true;
    //Game Over
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        //Keeps the player in bounds
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        // Check if the player is on the ground
        if (transform.position.y <= yRange)
        {
            isOnGround = true;
        }

        //This moves the player left and right based on input
        float HorizontalInput = Input.GetAxis("Horizontal");
        
        if (!gameOver)
        {
            playerRb.AddForce(Vector3.right * turnSpeed * HorizontalInput);
        }

        //This makes the player only jump once and when game is not over.
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    //For obstacles
    private void OnCollisionEnter(Collision collision)
    {
        if (lives > 0)
        {
            gameOver = false;
        }
        else if (lives == 0)
        {
            gameOver = true;
        }

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            lives -= 1;

            Debug.Log("Player collided with obstacle. Should reduce player health by 1. Current health: " + lives);

            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    //For powerups
    private void OnTriggerEnter(Collider other)
    {
        //For jump powerup
        if (other.gameObject.CompareTag("Powerup Jump"))
        {
            Debug.Log("Powerup collected!");
            Destroy(other.gameObject);

            if (Input.GetKeyDown(KeyCode.Space) && !gameOver && isOnGround)
            {
                playerRb.AddForce(Vector3.up * jumpForce * 2, ForceMode.Impulse);

                isOnGround = false;
            }
        }

        //For heart
        if (other.gameObject.CompareTag("Heart"))
        {
            Destroy(other.gameObject);

            if (lives < 2)
            {
                lives += 1;
                Debug.Log("Player collided with heart. Should increase player health by 1. Current health: " + lives);
            }
        }

        //For invicible powerup
        if (other.gameObject.CompareTag("Powerup Invincible"))
        {
            Destroy(other.gameObject);
        }

        //For fireball powerup
        if (other.gameObject.CompareTag("Powerup Fireball"))
        {
            Destroy(other.gameObject);
        }
    }
}