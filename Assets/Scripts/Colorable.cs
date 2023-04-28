using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Colorable : MonoBehaviour
{
    public static Action OnColorChanged;
    protected Color currentColor;
    public Material Color
    {
        get
        {
            return GetComponent<MeshRenderer>().sharedMaterial;
        }
        protected set
        {
            GetComponent<MeshRenderer>().material = value;
            OnColorChanged?.Invoke();
        }
    }
    public float Transparency
    {
        get {
            return Color.GetFloat("_Transparent");
        }
        protected set {
            Color.SetFloat("_Transparent", value);
        }
    }
}
