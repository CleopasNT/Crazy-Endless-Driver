using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public float jumpForce = 12000;
    public float xRange = 8;
    public float yRange = 0.54f;
    public float turnSpeed = 12000;
    private Rigidbody playerRb;
    private readonly float gravityModifier = 1;
    private bool isOnGround = true;
    //Player Input variables
    public Button leftButton;
    public Button rightButton;
    public TextMeshProUGUI leftButtonText;
    public TextMeshProUGUI rightButtonText;

    // Start is called before the first frame update
    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        Physics.gravity *= gravityModifier;
        //Player Input
        leftButton.onClick.AddListener(MoveLeft);
        rightButton.onClick.AddListener(MoveRight);
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

        //This makes the player only jump once.
        if (Input.GetKeyDown(KeyCode.Space) && isOnGround)
        {
            playerRb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isOnGround = false;
        }
    }

    //For obstacles
    private void OnCollisionEnter(Collision collision)
    {
        isOnGround = true;

        if (collision.gameObject.CompareTag("Obstacle"))
        {
            Debug.Log("Obstacle collided with the player. Should reduce player health.");
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
        playerRb.velocity = new Vector3(-turnSpeed, playerRb.velocity.y, playerRb.velocity.z);
    }

    public void MoveRight()
    {
        playerRb.velocity = new Vector3(turnSpeed, playerRb.velocity.y, playerRb.velocity.z);
    }
}