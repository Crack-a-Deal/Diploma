using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ColorGlass : MonoBehaviour
{
    public enum ColorState
    {
        Red,Green,Blue
    }
    [SerializeField] private ColorState color = ColorState.Red;

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
        inputActions.Player.Fire.performed += ColorShot;
        inputActions.Player.Fire.Enable();
    }
    private void Start()
    {
        gunCamera=Camera.main;
        cubes.AddRange(GameObject.FindGameObjectsWithTag("Red"));
        cubes.AddRange(GameObject.FindGameObjectsWithTag("Green"));
        cubes.AddRange(GameObject.FindGameObjectsWithTag("Blue"));

        CubeSwapColors(color.ToString());
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            NextColor();
            CubeSwapColors(color.ToString());
        }
    }
    private void NextColor()
    {
        if (color == ColorState.Red)
        {
            color = ColorState.Green;
            return;
        }

        if (color == ColorState.Green)
        {
            color = ColorState.Blue;
            return;
        }

        if (color == ColorState.Blue)
        {
            color = ColorState.Red;
            return;
        }
        Debug.Log($"Swap to {color}");
    }
    private void ColorShot(InputAction.CallbackContext obj)
    {
        RaycastHit hit;
        if (Physics.Raycast(gunCamera.transform.position, gunCamera.transform.forward, out hit))
        {
            if (hit.collider.TryGetComponent(out Cube target))
            {
                if(color1 == null)
                {
                    color1 = target.Color;
                    obj1 = target;
                }
                else
                {
                    color2 = target.Color;
                    obj2 = target;
                    SwapColors();
                }
            }
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
}
