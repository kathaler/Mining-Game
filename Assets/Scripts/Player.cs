using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed = 7f;
    public GameObject range;

    float halfScreenHorizontal;
    float playerHalfWidth;

    private Vector2 movement = new Vector2();
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        playerHalfWidth = transform.localScale.x / 2;
        halfScreenHorizontal = Camera.main.aspect * Camera.main.orthographicSize;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        GetInput();
        MoveCharacter(movement);
        range.transform.position = transform.position;
    }

    private void MoveCharacter(Vector2 movementVector)
    {
        movementVector.Normalize();
        // move the RigidBody2D instead of moving the Transform
        rb.velocity = movementVector * speed;
    }

    private void GetInput()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

}
