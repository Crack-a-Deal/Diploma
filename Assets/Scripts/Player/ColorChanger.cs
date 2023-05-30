using System;
using System.Collections.Generic;
using UnityEditor;
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

public class ColorChanger : MonoBehaviour
{
    public static Action<Color> OnGetColor;

    [Header("Level Colors")]
    [SerializeField] private ColorState colors;
    [SerializeField] private ColorState curentColor = ColorState.White;
    
    [Space]
    [SerializeField] private float transparency;

    private List<Cube> cubesList;
    private Camera raycastCamera;
    private PlayerInputActions inputActions;
    private Colorable tempObject;

    private void OnEnable()
    {
        inputActions.Player.Interact.performed += ChangeColor;
        inputActions.Player.Interact.Enable();
    }
    private void OnDisable()
    {
        inputActions.Player.Interact.Disable();
    }

    private void Awake()
    {
        inputActions = new PlayerInputActions();
    }

    private void Start()
    {
        raycastCamera = Camera.main;
        cubesList = new List<Cube>();
        FindAllObjectsWithTag();

        CubeSwapColors(curentColor.ToString());
    }

    // Функция находит все объекты на сцене с тегом
    private void FindAllObjectsWithTag()
    {
        string[] tags = Enum.GetNames(typeof(ColorState));

        foreach (string colorTag in tags)
        {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(colorTag);
            foreach (GameObject obj in objects)
            {
                cubesList.Add(obj.GetComponent<Cube>());
            }
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            curentColor = GetNextEnumColor(curentColor);
            CubeSwapColors(curentColor.ToString());
        }
    }

    // Функция принимает текущий цвет и возвращает следующей цвет в перечислении
    private ColorState GetNextEnumColor(ColorState currentState)
    {
        int current = (int)currentState;
        int next = current * 2;

        while (true)
        {
            if (next > 16)
                next = 1;
            if (colors.HasFlag((ColorState)next))
            {
                return (ColorState)next;
            }
            next = next * 2;
        }
    }

    // Функция возвращает выбранный объект на уровне
    private Collider GetSelectedObject()
    {
        if (raycastCamera == null)
            return null;
        RaycastHit hit;
        Physics.Raycast(raycastCamera.transform.position, raycastCamera.transform.forward, out hit);

        return hit.collider;
    }

    private void ChangeColor(InputAction.CallbackContext obj)
    {
        Collider selectedObject = GetSelectedObject();
        if (selectedObject == null)
            return;

        if (selectedObject.TryGetComponent(out Colorable target))
        {
            if (target.tag == "Dark")
                return;
            if (tempObject == null)
            {
                tempObject = target;
                OnGetColor?.Invoke(target.Color.GetColor("_Base_Color"));
                return;
            }
            OnGetColor?.Invoke(Color.white);
            SwapColors(tempObject, target);
            tempObject = null;
        }
    }

    // Функция меняет цвет у двух выбранных объектов
    private void SwapColors(Colorable first, Colorable second)
    {
        if (first.CompareTag("Dark") || second.CompareTag("Dark"))
            return;

        Material tempColor = first.Color;
        string tempTag = first.tag;
        
        first.SetColor(second.Color,second.tag);
        second.SetColor(tempColor,tempTag);

        CubeSwapColors(curentColor.ToString());
        AudioManager.PlaySound("ink");
    }    

    // Функция устанавливает прозрачность у объектов в зависимости от тега
    private void CubeSwapColors(string colorTag)
    {
        foreach(Cube cube in cubesList)
        {
            if(cube.tag == colorTag)
            {
                cube.SetTransparent(1);
            }
            else
            {
                cube.SetTransparent(transparency);
            }
        }
    }

    // Фукнция очищает выбранный цвет
    public void ClearColor()
    {
        tempObject=null;
        OnGetColor?.Invoke(Color.white);
    }

    private void OnGUI()
    {
        if (InputManager.isDev)
        {
            GUI.Label(new Rect(0, 120, 1000, 20), $"Current color - {curentColor}");
        }
    }
}
