using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public Rigidbody2D rg;

    public float jumpForce = 35f;
    public float moveForce = 10f;
    public float maxSpeed = 10f;

    private void FixedUpdate()
    {

    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.D))
        {
            if (rg.velocity.magnitude < maxSpeed)
            {
                rg.AddForce(moveForce * Vector2.right);
            }
        }

        if (Input.GetKey(KeyCode.A))
        {
            if (rg.velocity.magnitude < maxSpeed)
            {
                rg.AddForce(moveForce * Vector2.left);
            }
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            rg.AddForce(jumpForce * Vector2.up);

        }
    }
}
