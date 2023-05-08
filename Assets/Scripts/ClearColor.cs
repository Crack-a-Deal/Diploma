using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClearColor : MonoBehaviour
{
    private ColorChanger changer;

    private void Awake()
    {
        changer = FindObjectOfType<ColorChanger>();
    }
    private void OnTriggerEnter(Collider other)
    {
        changer.ClearColor();
    }
}
