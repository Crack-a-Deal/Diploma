using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorButton : MonoBehaviour
{
    [SerializeField] private Door linkedDoor;
    [SerializeField] private Cube cube;

    private void Update()
    {
        if (!linkedDoor.IsOpen && Equals(cube.Color, linkedDoor.Correct—olor) && cube.Transparency == 1)
        {
            linkedDoor.Open(Vector3.zero);
        }
        if(linkedDoor.IsOpen && cube.Transparency != 1)
        {
            linkedDoor.Close();
        }
        if (linkedDoor.IsOpen && !Equals(cube.Color, linkedDoor.Correct—olor))
        {
            linkedDoor.Close();
        }

    }
}
