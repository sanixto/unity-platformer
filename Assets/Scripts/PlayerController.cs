using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed = 5f;

    private Rigidbody2D rb; 

    private void Awake() {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (Input.GetButton("Horizontal")) {
            Run();
        }
    }

    private void Run() {
        Vector3 curPosition = transform.position;
        Vector3 dir = transform.right * Input.GetAxis("Horizontal");
        transform.position = Vector3.MoveTowards(curPosition, curPosition + dir, speed * Time.deltaTime);
    }
}
