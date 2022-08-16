using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //This also works with joystick, PS this is topdown movement

    private Rigidbody2D rb;
    public float MoveSpeed = 15f;
    float vertical;
    float horizontal;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // TODO: Need to change this to use the new Unity input module
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical"); 
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(horizontal * MoveSpeed, vertical * MoveSpeed);
    }
}