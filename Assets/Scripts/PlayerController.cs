using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private int lives = 5;
    [SerializeField] private float jumpForce = 4.6f; 

    private Vector2 startPosition = new Vector2(-15, 0);
    private bool isGrounded;
    private readonly float deathDelay = 0.3f;
    private Rigidbody2D rb; 
    private CapsuleCollider2D feetCollider;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        feetCollider = GetComponent<CapsuleCollider2D>();
    }

    private void Start() {
        Debug.Log("Your lives: " + lives);
    }

    void FixedUpdate () {
        CheckGround();
    }

    void Update()
    {
        if (Input.GetButton("Horizontal")) {
            Run();
        }
        if (isGrounded && Input.GetButton("Jump")) {
            Jump();
        }
    }

    private void OnTriggerEnter2D (Collider2D other) {
        if (other.tag == "Water") {
            GetDamage();
            if (lives == 0) {
                Die();
                return;
            }
            transform.position = startPosition;
        }
    }

    private void Run() {
        Vector3 curPosition = transform.position;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(curPosition, curPosition + dir, speed * Time.deltaTime);
    }

    private void Jump() {
        rb.velocity += new Vector2(0f, jumpForce);
    }

    private void CheckGround() {
        LayerMask groundMask = LayerMask.GetMask("Ground");
        isGrounded = feetCollider.IsTouchingLayers(groundMask);
    }

    private void GetDamage() {
        lives -= 1;
        Debug.Log("Your lives: " + lives);
    }

    private void Die() {
        Destroy(rb.GameObject(), deathDelay);
        Debug.Log("Game over");
    }
}
