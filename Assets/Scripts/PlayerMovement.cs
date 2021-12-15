/*  
    PlayerMovement.cs

    This script is responsible for allowing the player to move around
    in the game scene with the keyboard.
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveH;
    private float moveV;
    public CharacterController controller;
    private float speed = 30.0f; // Movement speed multiplier

    void Start() {
        Rigidbody rigidBody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Get wasd/controller input
        moveH = Input.GetAxisRaw("Horizontal");
        moveV = Input.GetAxisRaw("Vertical");

        // Move player based on input
        Vector3 move = transform.right * moveH + transform.forward * moveV;
        controller.Move(move * speed * Time.deltaTime);

        // Fly up
        if (Input.GetKey(KeyCode.Space))
        {
            controller.Move(transform.up * speed * Time.deltaTime);
        }

        // Fly down
        if (Input.GetKey(KeyCode.LeftShift))
        {
            controller.Move(-transform.up * speed * Time.deltaTime);
        }

       
    }
}
