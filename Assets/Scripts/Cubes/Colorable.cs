using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public abstract class Colorable : MonoBehaviour
{
    protected Material defaultColor;
    protected string defaultTag;
    public Material Color
    {
        get
        {
            return GetComponent<MeshRenderer>().sharedMaterial;
        }
        protected set
        {
            GetComponent<MeshRenderer>().material = value;
        }
    }
    public float Transparency
    {
        get
        {
            return Color.GetFloat("_Transparent");
        }
        protected set {
            Color.SetFloat("_Transparent", value);
        }
    }
    public void SetColor(Material newMaterial,string newTag)
    {
        defaultColor = newMaterial;
        defaultTag = newTag;

        GetComponent<MeshRenderer>().material = newMaterial;
        tag= newTag;
    }
}
