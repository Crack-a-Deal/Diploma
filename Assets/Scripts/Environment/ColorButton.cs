using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : Colorable
{
    [SerializeField] private Door linkedDoor;

    private void Update()
    {
        if (!linkedDoor.IsOpen && Equals(Color, linkedDoor.Correct—olor) && Transparency == 1)
        {
            linkedDoor.Open(Vector3.zero);
        }
        if(linkedDoor.IsOpen && Transparency != 1)
        {
            linkedDoor.Close();
        }
        if (linkedDoor.IsOpen && !Equals(Color, linkedDoor.Correct—olor))
        {
            linkedDoor.Close();
        }

    }
    //private void OnGUI()
    //{
    //    if (InputManager.isDev)
    //    {
    //        GUI.Label(new Rect(0, 140, 1000, 20), $"stand color - {Color.name} || end color - {linkedDoor.Color}");
    //    }
    //}
}
