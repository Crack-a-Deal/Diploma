using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Vector2 mouseSensitivity;
    [SerializeField] private Transform orientation;
    private Vector2 mouseRotation;
    [SerializeField] private bool isCursorLock;

    private void Start()
    {
        Cursor.lockState = isCursorLock ? CursorLockMode.Locked : CursorLockMode.Confined;
    }

    private void Update()
    {
        float mouseX = Input.GetAxisRaw("Mouse X");
        float mouseY = Input.GetAxisRaw("Mouse Y");

        mouseRotation.y += mouseX;

        mouseRotation.x -= mouseY;
        mouseRotation.x = Mathf.Clamp(mouseRotation.x, -90f, 90f);

        transform.rotation = Quaternion.Euler(mouseRotation.x, mouseRotation.y, 0);
        orientation.rotation = Quaternion.Euler(0, mouseRotation.y, 0);
    }
}
