using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMovement : MonoBehaviour
{
    [Header("Movement")]
    public float movementSpeed;
    public float jumpForce;
    [Range(0f, 1f)] public float airMovementMultiplier = 0.5f;
    public float movementSpeedIncrease = 1.05f;
    public float maximumMovementSpeed = 10;

    private Rigidbody rb;
    private Animator animator;
    private int currentLane;
    private bool canJump;

    // Start is called before the first frame update
    void Start()
    {
        canJump = true;
        currentLane = 0;
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
    }

    // FixedUpdate makes the movement less jittery
    private void FixedUpdate()
    {
        // Moving the wolf forward
        transform.Translate(movementSpeed * Time.deltaTime * (canJump ? 1 : airMovementMultiplier), 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        // Jump logic
        if (Input.GetKeyDown(KeyCode.Space) && canJump)
        {
            animator.SetTrigger("Jump");
            rb.AddForce(0, jumpForce, 0, ForceMode.Impulse);
            canJump = false;
        }

        // Switching lanes through key presses
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetTrigger("Move Left");
            currentLane--;
        }
        else if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetTrigger("Move Right");
            currentLane++;
        }

        if (rb.velocity.y < 0) Physics.gravity = new Vector3(0, -9.8f * 2, 0);

        // Clamping the values to the maximum left and right lanes
        currentLane = Mathf.Clamp(currentLane, -GameManager.maximumLeftLanes, GameManager.maximumRightLanes);

        // Setting the position to the current lane
        Vector3 tempPosition = transform.position;
        tempPosition.z = -currentLane * GameManager.distanceBetweenLanes;
        transform.position = tempPosition;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If the player touches the ground, play the landing animation and allow the player to jump
        if (collision.gameObject.CompareTag("Ground") && !canJump)
        {
            animator.SetTrigger("Land");
            canJump = true;
            Physics.gravity = new Vector3(0, -9.8f, 0);
        }
        else if (collision.gameObject.CompareTag("Obstacle"))
        {
            GameManager.SaveScore();
            GameManager.LoadTitleScreen();
        }
    }

    // Using triggers for collectables so there is no collision with them
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Collectable>() != null)
        {
            // Increasing the movement speed based on how many collectables have been collected
            if (movementSpeed < maximumMovementSpeed) movementSpeed *= movementSpeedIncrease;
            else movementSpeed = maximumMovementSpeed;

            Collectable collectable = other.gameObject.GetComponent<Collectable>();
            collectable.Collect();
        }
    }
}
