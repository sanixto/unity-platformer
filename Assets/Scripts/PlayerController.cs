using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;
    [SerializeField] private float jumpForce = 4.6f; 

    private bool isGrounded;
    private Rigidbody2D rb; 
    private CapsuleCollider2D feetCollider;

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
        feetCollider = GetComponent<CapsuleCollider2D>();
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
}
