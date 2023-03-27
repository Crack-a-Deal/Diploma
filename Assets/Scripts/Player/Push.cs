using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class Push : MonoBehaviour
{
    [SerializeField] private Transform orientation;
    [SerializeField] private float forceMagnitude;

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        var rigidBody = hit.collider.attachedRigidbody;

        if (rigidBody != null && hit.gameObject.tag=="Red")
        {
            var forceDirection = orientation.forward;
            forceDirection.y = 0;
            forceDirection.Normalize();

            rigidBody.AddForceAtPosition(forceDirection * forceMagnitude, transform.position, ForceMode.Impulse);


        }
    }
}
