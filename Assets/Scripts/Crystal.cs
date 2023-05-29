using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crystal : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Level Finished");
        //AudioManager.PlaySound("teleport");
        LevelManager.LoadNextLevel();
    }
}
