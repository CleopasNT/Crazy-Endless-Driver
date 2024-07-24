using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public float jumpForce = 12000;
    public float xRange = 8;
    private float turnSpeed = 12000.0f;
    private Rigidbody playerRb;
    private float gravityModifier = 1;
    private bool isOnGround = true;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
    }

    // Update is called once per frame
    void Update()
    {
        //These 2 if statements keep the player inbound
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
        }

        if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
        }

        //This moves the player left and right based on input
        float HorizontalInput = Input.GetAxis("Horizontal");
        playerRb.AddForce(Vector3.right * turnSpeed * HorizontalInput);

        //This makes the player only jump once. Icluding the OnCollisionEnter method
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle collided with the player. Should reduce player health.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //For jump powerup
        if (other.gameObject.CompareTag("Powerup Jump"))
        {
            Destroy(other.gameObject);
        }
        //For heart
        if (other.gameObject.CompareTag("Heart"))
        {
            Destroy(other.gameObject);
        }
        //For magnet powerup
        if (other.gameObject.CompareTag("Powerup Magnet"))
        {
            Destroy(other.gameObject);
        }
        //For invicible powerup
        if (other.gameObject.CompareTag("Powerup Invincible"))
        {
            Destroy(other.gameObject);
        }
        //For coins
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
    }
}
