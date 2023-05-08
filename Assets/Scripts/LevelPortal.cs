using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LevelPortal : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private Transform exitPortal;

    //public void Warp()
    //{
    //    Debug.Log("Teleport");
    //    player.transform = Vector3.zero;
    //    //SceneManager.LoadScene(location.name);
    //}
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Teleport");
        Debug.Log(player.transform.position);
    }
}
