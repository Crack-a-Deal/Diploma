using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube : MonoBehaviour
{
    public Material Color
    {
        get
        {
            return gameObject.GetComponent<MeshRenderer>().material;
        }
        set
        {
            gameObject.GetComponent<MeshRenderer>().material = value;
        }
    }
}
