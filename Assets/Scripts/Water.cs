using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Water : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] private SceneTransition transition;
    [SerializeField] private float blackoutTime;

    private void OnTriggerEnter(Collider other)
    {
        AudioManager.PlaySound("water_splash");
        other.GetComponent<PlayerMovement>().canMove = false;
        other.GetComponent<PlayerMovement>().canJump = false;
        StartCoroutine(Respawn(other));
    }
    private IEnumerator Respawn(Collider player)
    {
        yield return StartCoroutine(transition.Transition(blackoutTime));

        CharacterController controller = player.GetComponent<CharacterController>();
        controller.enabled = false;
        controller.transform.position = spawnPoint.position;
        controller.enabled = true;
        player.GetComponent<PlayerMovement>().canMove = true;
        player.GetComponent<PlayerMovement>().canJump = true;
        FindObjectOfType<PlayerCamera>().transform.rotation=spawnPoint.rotation;
        yield return null;
    }
}
