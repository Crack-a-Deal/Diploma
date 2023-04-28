using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[Flags]
public enum ColorState
{
    White = 1,
    Green = 2,
    Red = 4,
    Blue = 8,
    Yellow = 16
}

public class ColorGlass : MonoBehaviour
{
    [Header("UPDATE")]
    [SerializeField] private ColorState colors;

    [SerializeField] private ColorState curentColor = ColorState.White;


    [SerializeField] private List<Cube> cubes;
    private Camera gunCamera;
    private PlayerInputActions inputActions;

    [SerializeField] private Material color1;
    [SerializeField] private Material color2;
    [SerializeField] private float transparency;
    private Cube obj1;
    private Cube obj2;

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }
    private void OnEnable()
    {
        inputActions.Player.FirsColor.performed += SelectFirstColor;
        inputActions.Player.FirsColor.Enable();

        inputActions.Player.SecondColor.performed += SelectSecondColor;
        inputActions.Player.SecondColor.Enable();
    }
    private void Start()
    {
        gunCamera =Camera.main;
        FindAllObjectsWithTag();

        CubeSwapColors(curentColor.ToString());
    }

    private void FindAllObjectsWithTag()
    {
        string[] tags = colors.ToString().Split(", ");
        
        foreach (string colorTag in tags)
        {
            GameObject[]objects =  GameObject.FindGameObjectsWithTag(colorTag);
            foreach (GameObject obj in objects)
            {
                cubes.Add(obj.GetComponent<Cube>());
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            curentColor = NextEnumColor(curentColor);
            CubeSwapColors(curentColor.ToString());
        }
    }
    private ColorState NextEnumColor(ColorState state)
    {
        switch (state)
        {
            case ColorState.White: return ColorState.Green;
            case ColorState.Green: return ColorState.Red;
            case ColorState.Red: return ColorState.Blue;
            case ColorState.Blue: return ColorState.Yellow;
            case ColorState.Yellow: return ColorState.White;
            default: return ColorState.White;
        }
    }
    private void SelectFirstColor(InputAction.CallbackContext obj)
    {
        RaycastHit hit;

        if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out hit))
        {
            if (hit.collider.TryGetComponent(out Cube target))
            {
                color1 = target.Color;
                obj1 = target;
            }
        }
        if (color1 != null && color2 != null)
        {
            SwapColors();
        }
    }

    private void SelectSecondColor(InputAction.CallbackContext obj)
    {
        RaycastHit hit;
        if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out hit))
        {
            if (hit.collider.TryGetComponent(out Cube target))
            {
                color2 = target.Color;
                obj2 = target;
            }
        }
        if(color1 != null && color2 != null)
        {
            SwapColors();
        }
    }
    private void SwapColors()
    {
        obj1.Color = color2;
        obj2.Color = color1;

        string temp = obj1.tag;
        obj1.tag = obj2.tag;
        obj2.tag = temp;

        CubeSwapColors(curentColor.ToString());

        obj1 = null;
        obj2=null;

        color1 = null;
        color2 = null;
    }


    private void CubeSwapColors(string tag1)
    {
        foreach(Cube cube in cubes)
        {
            if(cube.tag == tag1)
            {
                cube.SetTransparent(1);
            }
            else
            {
                cube.SetTransparent(transparency);
            }
        }
    }
    private void OnGUI()
    {
        if (InputManager.isDev)
        {
            GUI.Label(new Rect(0, 100, 1000, 20), $"First - {color1}, Second - {color2}");
            GUI.Label(new Rect(0, 120, 1000, 20), $"Current color - {curentColor}");
        }
    }
}
