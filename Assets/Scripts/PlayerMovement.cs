using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float moveH;
    private float moveV;
    public CharacterController controller;
    private float speed = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moveH = Input.GetAxisRaw("Horizontal");
        moveV = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * moveH + transform.forward * moveV;

        controller.Move(move * speed * Time.deltaTime);
        
    }
}
