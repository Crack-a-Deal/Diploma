using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Vector2 mouseSensitivity;
    [SerializeField] private Transform orientation;
    private Vector2 mouseRotation;
    [SerializeField] private bool isCursorLock;

    private void Awake()
    {
        PauseController.OnPause += ShowCursor;
        PauseController.OnResume += ShowCursor;
    }
    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void Update()
    {
        if (Cursor.lockState == CursorLockMode.Locked)
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
    private void ShowCursor()
    {
        isCursorLock = !isCursorLock;
        Cursor.lockState = isCursorLock ? CursorLockMode.Locked : CursorLockMode.Confined;
    }
}
