using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    public GameObject player;
    private float mousePitch;
    private float mouseYaw;
    private float mouseSensitivity = 3.0f;

    void Start()
    {
        player = this.transform.parent.gameObject;
    }

    void Update()
    {
        mousePitch -=  Input.GetAxisRaw("Mouse Y") * mouseSensitivity;
        mouseYaw += Input.GetAxisRaw("Mouse X") * mouseSensitivity;
        transform.eulerAngles = new Vector3(mousePitch, mouseYaw, 0.0f);
    }
}
