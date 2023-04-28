using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelPortal : MonoBehaviour
{
    [SerializeField] private SceneAsset location;

    public void Warp()
    {
        SceneManager.LoadScene(location.name);
    }
    private void OnTriggerEnter(Collider other)
    {
        Warp();
    }
}
