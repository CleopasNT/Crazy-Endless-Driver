using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float jumpForce = 12000;
    public float xRange = 8;
    public float yRange = 0.54f;
    public float turnSpeed = 12000;
    public GameObject projectilePrefab;
    private int lives = 3;
    private bool jumpPowerupActive = false;
    private bool invinciblePowerupActive = false;
    private bool fireballPowerupActive = false;
    public float flyHeight;
    private Rigidbody playerRb;
    private readonly float gravityModifier = 1;
    public bool isOnGround = true;

    // UI
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;
    public Button projectButton;

    // Game Over
    public bool gameOver = false;

    // Movement state
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        leftButton.onClick.AddListener(() => { isMovingLeft = true; });
        rightButton.onClick.AddListener(() => { isMovingRight = true; });
        jumpButton.onClick.AddListener(JumpButtonPressed); // Change to a new method for jumping
        // projectButton.onClick.AddListener(Project);

        // Add listeners for button release
        leftButton.onClick.AddListener(OnLeftButtonReleased);
        rightButton.onClick.AddListener(OnRightButtonReleased);
    }

    private void JumpButtonPressed()
    {
        if (isOnGround && !gameOver)
        {
            if (jumpPowerupActive)
            {
                Jump(2); // Jump with powerup multiplier
            }
            else
            {
                Jump(1); // Normal jump
            }
            isOnGround = false; // Set to false immediately after jumping
        }
    }

    void OnLeftButtonReleased()
    {
        isMovingLeft = false;
    }

    void OnRightButtonReleased()
    {
        isMovingRight = false;
    }

    // Update is called once per frame
    void Update()
    {
        // Keeps the player in bounds
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
        else
        {
            isOnGround = false; // Reset to false when not on the ground
        }

        // This moves the player left and right based on button presses
        if (!gameOver)
        {
            if (isMovingLeft)
            {
                playerRb.AddForce(Vector3.left * turnSpeed * Time.deltaTime);
            }

            if (isMovingRight)
            {
                playerRb.AddForce(Vector3.right * turnSpeed * Time.deltaTime);
            }
        }

        // Jumping logic
        /*if (Input.GetKeyDown(KeyCode.Space) && isOnGround && !gameOver)
        {
            if (jumpPowerupActive)
            {
                Jump(2); // Jump with powerup multiplier
            }
            else
            {
                Jump(1); // Normal jump
            }
            isOnGround = false;
        }*/

        // Invincibility logic
        if (invinciblePowerupActive && !gameOver)
        {
            flyHeight = 6;
            transform.position = new Vector3(transform.position.x, flyHeight, transform.position.z);
            playerRb.useGravity = false;
        }
        else if (!invinciblePowerupActive && !gameOver)
        {
            playerRb.useGravity = true;
        }

        // Fireball logic
        if (fireballPowerupActive && Input.GetKeyDown(KeyCode.E) && !gameOver)
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }

    void Jump(int multiplier)
    {
        playerRb.AddForce(Vector3.up * jumpForce * multiplier, ForceMode.Impulse);
    }

    // For obstacles
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
            Debug.Log("Player collided with obstacle. Current health: " + lives);
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    // For powerups
    private void OnTriggerEnter(Collider other)
    {
        // For jump powerup
        if (other.gameObject.CompareTag("Powerup Jump"))
        {
            Destroy(other.gameObject);
            jumpPowerupActive = true;
            StartCoroutine(ApplyPowerup("Jump", 15, 2));
        }

        // For heart
        if (other.gameObject.CompareTag("Heart"))
        {
            Destroy(other.gameObject);
            if (lives < 3)
            {
                lives += 1;
                Debug.Log("Player collided with heart. Current health: " + lives);
            }
        }

        // For invincible powerup
        if (other.gameObject.CompareTag("Powerup Invincible"))
        {
            Destroy(other.gameObject);
            StartCoroutine(ApplyPowerup("Invincible", 10));
        }

        // For fireball powerup
        if (other.gameObject.CompareTag("Powerup Fireball"))
        {
            Destroy(other.gameObject);
            StartCoroutine(ApplyPowerup("Fireball", 15));
        }
    }

    private IEnumerator ApplyPowerup(string powerupType, float duration, float multiplier = 1)
    {
        switch (powerupType)
        {
            case "Jump":
                jumpPowerupActive = true;
                break;
            case "Invincible":
                invinciblePowerupActive = true;
                break;
            case "Fireball":
                fireballPowerupActive = true;
                break;
            default:
                yield break;
        }

        yield return new WaitForSeconds(duration);

        switch (powerupType)
        {
            case "Jump":
                jumpPowerupActive = false;
                break;
            case "Invincible":
                invinciblePowerupActive = false;
                break;
            case "Fireball":
                fireballPowerupActive = false;
                break;
        }
    }
}
