using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoBehaviour
{
    [SerializeField] private Transform portalPosition;
    private void OnTriggerEnter(Collider other)
    {
        AudioManager.PlaySound("teleport");
        CharacterController controller = other.GetComponent<CharacterController>();

        controller.enabled = false;
        controller.transform.position = portalPosition.position;
        controller.enabled = true;

    }
}
