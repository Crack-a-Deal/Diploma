using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ColorCursor : MonoBehaviour
{
    [SerializeField] private Image first;

    private void OnEnable()
    {
        ColorChanger.OnGetColor += ChangeCursorColor;
    }
    private void OnDisable()
    {
        ColorChanger.OnGetColor -= ChangeCursorColor;
    }

    private void ChangeCursorColor(Color color)
    {
        first.color = color;
    }
}
