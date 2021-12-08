using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    private float mouseX;
    private float mouseY;
    private float rotationX = 0.0f;

    private float mouseSensitivity = 400.0f;
    public Transform playerTransform;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        mouseX = Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;
        mouseY = Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;

        rotationX -= mouseY;
        rotationX = Mathf.Clamp(rotationX, -90.0f, 90.0f);

        transform.localRotation = Quaternion.Euler(rotationX, 0.0f, 0.0f);
        playerTransform.Rotate(Vector3.up * mouseX);
    }
}
