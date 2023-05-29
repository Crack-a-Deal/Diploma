using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI;

public abstract class Colorable : MonoBehaviour
{
    public static Action OnColorChanged;
    public static Action OnTransparencyChanged;
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
    public void SetColor(Material newMaterial)
    {
        GetComponent<MeshRenderer>().material = newMaterial;
        OnColorChanged?.Invoke();
    }
}
