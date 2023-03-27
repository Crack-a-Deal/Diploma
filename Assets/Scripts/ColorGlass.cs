using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColorGlass : MonoBehaviour
{
    public enum ColorState
    {
        White, Green,Red,Blue,Yellow
    }


    [SerializeField] private ColorState color = ColorState.White;
    [SerializeField] private ColorState[] colors;


    [SerializeField] private List<GameObject> cubes;
    private Camera gunCamera;
    private PlayerInputActions inputActions;

    [SerializeField] private Material color1;
    [SerializeField] private Material color2;
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
        gunCamera=Camera.main;
        FindAllObjectsWithTag();

        CubeSwapColors(color.ToString());
    }

    private void FindAllObjectsWithTag()
    {
        foreach(var colorTag in colors)
        {
            cubes.AddRange(GameObject.FindGameObjectsWithTag(colorTag.ToString()));
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            color=NextEnumColor(color);
            CubeSwapColors(color.ToString());
        }
    }
    private ColorState NextEnumColor(ColorState state)
    {
        switch (state)
        {
            case ColorState.White: return ColorState.Green;
            case ColorState.Green: return ColorState.White;
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

        CubeSwapColors(color.ToString());

        obj1 = null; obj2=null;
        color1 = null; color2 = null;
    }
    private void CubeSwapColors(string tag1)
    {
        foreach(GameObject cube in cubes)
        {
            if(cube.tag == tag1)
            {
                cube.gameObject.SetActive(true);
            }
            else
            {
                cube.gameObject.SetActive(false);
            }
        }
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(0, 100, 1000, 20), $"First - {color1}, Second - {color2}");

    }
}
