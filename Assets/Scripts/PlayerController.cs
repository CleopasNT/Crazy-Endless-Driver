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

    //UI
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;
    public Button projectButton;

    //Game Over
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        leftButton.onClick.AddListener(MoveLeft);
        rightButton.onClick.AddListener(MoveRight);
        jumpButton.onClick.AddListener(() => Jump(1)); // Pass 1 as multiplier for normal jump
        //projectButton.onClick.AddListener(Project);
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
            if (jumpPowerupActive)
            {
                Jump(2); // Jump with powerup multiplier
            }
            else
            {
                Jump(1); // Normal jump
            }
            isOnGround = false;
        }
        //Invinciblity logic
        if (invinciblePowerupActive == true && gameOver == false)
        {
            flyHeight = 6;
            transform.position = new Vector3(transform.position.x, flyHeight, transform.position.z);

            GetComponent<Rigidbody>().useGravity = false;
        }
        else if (invinciblePowerupActive == false && gameOver == false)
        {
            GetComponent<Rigidbody>().useGravity = true;
        }

        //Fireball logic
        if (fireballPowerupActive == true && Input.GetKeyDown(KeyCode.E) && gameOver == false)
        {
            Instantiate(projectilePrefab, transform.position, projectilePrefab.transform.rotation);
        }
    }

    void MoveLeft()
    {
        playerRb.velocity = new Vector3(-turnSpeed * Time.deltaTime, playerRb.velocity.y);
    }

    void MoveRight()
    {
        playerRb.velocity = new Vector3(turnSpeed * Time.deltaTime, playerRb.velocity.y);
    }

    void Jump(int multiplier)
    {
        playerRb.AddForce(Vector3.up * jumpForce * multiplier, ForceMode.Impulse);
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

            Debug.Log(
                "Player collided with obstacle. Should reduce player health by 1. Current health: "
                    + lives
            );

            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }

    //For powerups
    private void OnTriggerEnter(Collider other)
    {
        //For jump powerup
        if (other.gameObject.CompareTag("Powerup Jump"))
        {
            Destroy(other.gameObject);

            jumpPowerupActive = true;

            StartCoroutine(ApplyPowerup("Jump", 15, 2));
        }

        //For heart
        if (other.gameObject.CompareTag("Heart"))
        {
            Destroy(other.gameObject);

            if (lives < 3)
            {
                lives += 1;
                Debug.Log(
                    "Player collided with heart. Should increase player health by 1. Current health: "
                        + lives
                );
            }
        }

        //For invicible powerup
        if (other.gameObject.CompareTag("Powerup Invincible"))
        {
            Destroy(other.gameObject);

            StartCoroutine(ApplyPowerup("Invincible", 10));
        }

        //For fireball powerup
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
