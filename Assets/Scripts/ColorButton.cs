using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : Colorable
{
    [SerializeField] private Door door;
    [SerializeField] private Cube standCube;
    [SerializeField] private Material end;

    private void Update()
    {
        if (!door.IsOpen && Equals(standCube.Color, end) && standCube.Color.GetFloat("_Transparent") == 1)
        {
            door.Open(Vector3.zero);
        }
        if(door.IsOpen && standCube.Color.GetFloat("_Transparent") != 1)
        {
            door.Close();
        }
        if (door.IsOpen && !Equals(standCube.Color, end))
        {
            door.Close();
        }

    }
    private void OnGUI()
    {
        if (InputManager.isDev)
        {
            GUI.Label(new Rect(0, 140, 1000, 20), $"stand color - {standCube.Color.name} || end color - {end}");
        }
    }
}
