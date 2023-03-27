using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class YellowCube : MonoBehaviour
{
    public float up;
    public bool isFly = false;
    private Vector3 minPosition;
    private Vector3 maxPosition;
    private Vector3 target;
    [SerializeField] private float speed;
    private void Start()
    {
        minPosition= transform.position;
        maxPosition = transform.position;
        maxPosition.y = maxPosition.y + up;

        target = maxPosition;
    }

    private void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if(transform.position == maxPosition)
        {
            target= minPosition;
        }
        if(transform.position == minPosition)
        {
            target = maxPosition;
        }
    }
}
