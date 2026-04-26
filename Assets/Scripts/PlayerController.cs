using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;

public class PlayerController : MonoBehaviour
{
    // reference to the rigidbody
    private Rigidbody rb;

    // number of pickups
    private int numPickups;
    // count of pickups
    private int count;

    // movement x and y
    private float movementX;
    private float movementY;
    // is grounded
    private bool isGrounded;

    // jump force
    public float jumpForce = 12f;
    // speed
    public float speed = 0;

    // reference to the count text
    public TextMeshProUGUI countText;
    // reference to the win text object
    public GameObject winTextObject;
    // reference to the reset button
    public GameObject resetButton;

    // reference to the explosion fx
    public GameObject explosionFx;
    // reference to the pickup fx
    public GameObject pickupFx;

    // reference to the camera transform
    public Transform cameraTransform;

    // reference to the explosion sound
    public AudioSource explosionSound;
    // reference to the pickup sound
    public AudioSource pickupSound;
    // reference to the jump sound
    public AudioSource jumpSound;

    // reference to the ocean object
    public GameObject oceanObject;
    // difficulty multiplier
    public float difficultyMultiplier = 1f;

    // Start is called before the first frame update
    void Start()
    {
        // get rigidbody
        rb = GetComponent<Rigidbody>();
        // set count to 0
        count = 0;
        // set number of pickups to the number of pickups in the scene
        numPickups = GameObject.FindGameObjectsWithTag("PickUp").Length;

        // set win text object to inactive

        winTextObject.SetActive(false);
        // set count text
        SetCountText();
    }

    private void FixedUpdate()
    {
        // Input vector
        Vector3 input = new Vector3(movementX, 0f, movementY);

        // Convert input to camera-relative
        Vector3 camForward = cameraTransform.forward;
        Vector3 camRight = cameraTransform.right;

        // Flatten camera vectors on the XZ plane
        camForward.y = 0f;
        camRight.y = 0f;
        camForward.Normalize();
        camRight.Normalize();

        // Combine input with camera orientation
        Vector3 movement = camForward * input.z + camRight * input.x;

        rb.AddForce(movement * speed);
    }

    void Update()
    {
        // if player is below the ocean, die
        if (transform.position.y < oceanObject.transform.position.y) {
            PlayerDies();
        }
    }

    void OnMove(InputValue movementValue)
    {
        // get movement vector
        Vector2 movementVector = movementValue.Get<Vector2>();

        // use input to update player
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnJump(InputValue value)
    {
        // if jump is pressed
        if (value.isPressed)
        {
            // and player is grounded, jump
            if (isGrounded)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                isGrounded = false;
                jumpSound.Play();
            }
        }
    }

    // set count text
    void SetCountText()
    {
        // update text when picking up object
        countText.text = "Collected: " + count.ToString() + "/" + numPickups;

        // if player has collected all pickups, win
        if (count >= numPickups)
        {
            PlayerWins();
        }
    }

    void PlayerWins()
    {
        // set win text object to active
        winTextObject.SetActive(true);
        // set reset button to active
        resetButton.SetActive(true);

        // find enemy and instantiate explosion fx
        GameObject enemy = GameObject.FindGameObjectWithTag("Enemy");
        Instantiate(explosionFx, enemy.transform.position, explosionFx.transform.rotation);
        // destroy enemy
        Destroy(enemy);

        // play explosion sound
        explosionSound.Play();
    }

    void PlayerDies()
    {
        // destroy player
        Destroy(gameObject);

        // set win text object to "You Lose!"
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";

        // set win text object to active
        winTextObject.SetActive(true);

        // set reset button to active
        resetButton.SetActive(true);

        // find all enemies and set game over to true
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
        foreach (GameObject enemy in enemies) {
            enemy.GetComponent<Animator>().SetBool("GameOver", true);
            enemy.GetComponent<Animator>().SetBool("GameStarted", false);
        }

        // instantiate explosion fx
        Instantiate(explosionFx, transform.position, explosionFx.transform.rotation);

        // play explosion sound
        explosionSound.Play();
    }

    void OnTriggerEnter(Collider other)
    {
        // make object disappear when picked up
        if (other.gameObject.CompareTag("PickUp"))
        {
            // make object disappear
            other.gameObject.SetActive(false);

            // increment count
            count = count + 1;

            // set count text
            SetCountText();

            // instantiate pickup fx
            var currentPickupFX = Instantiate(pickupFx, other.transform.position, Quaternion.identity);

            // destroy pickup fx after 3 seconds
            Destroy(currentPickupFX, 3);

            // play pickup sound
            pickupSound.Play();
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // die when player collides with enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            PlayerDies();
        }

        // set grounded to true when player collides with ground
        else if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }
}
