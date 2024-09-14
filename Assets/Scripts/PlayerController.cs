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
    private int lives = 2; //total lives is 2, but last life is -1, so total is 3
    private Rigidbody playerRb;
    private readonly float gravityModifier = 1;
    private bool isOnGround = true;
    //Player Input variables
    public Button leftButton;
    public Button rightButton;
    public Button jumpButton;
    public TextMeshProUGUI leftButtonText;
    public TextMeshProUGUI rightButtonText;
    public TextMeshProUGUI jumpButtonText;
    //Game Over
    public bool gameOver = false;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;

        //Player Input
        leftButton.onClick.AddListener(() => StartCoroutine(ButtonCooldown(MoveLeft, leftButton)));
        rightButton.onClick.AddListener(() => StartCoroutine(ButtonCooldown(MoveRight, rightButton)));
        jumpButton.onClick.AddListener(() => StartCoroutine(ButtonCooldown(Jump, jumpButton)));
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
    }

    //For obstacles
    private void OnCollisionEnter(Collision collision)
    {
        if (lives > 0)
        {
            gameOver = false;
        }
        else if (lives <= 0)
        {
            gameOver = true;
            RemoveListeners();
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
            Destroy(other.gameObject);
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

    public void MoveLeft()
    {
        if (!gameOver)
        {
            playerRb.velocity = new Vector3(-turnSpeed, playerRb.velocity.y, playerRb.velocity.z);
        }
    }

    public void MoveRight()
    {
        if (!gameOver)
        {
            playerRb.velocity = new Vector3(turnSpeed, playerRb.velocity.y, playerRb.velocity.z);
        }
    }

    public void Jump()
    {
        if (!gameOver && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    private IEnumerator ButtonCooldown(System.Action action, Button button)
    {
        button.interactable = false;
        action();
        yield return new WaitForSeconds(0.5f); // Adjust the cooldown duration as needed
        button.interactable = true;
    }

    private void RemoveListeners()
    {
        leftButton.onClick.RemoveListener(MoveLeft);
        rightButton.onClick.RemoveListener(MoveRight);
        jumpButton.onClick.RemoveListener(Jump);
    }
}