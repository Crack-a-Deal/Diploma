using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

public class Cube : MonoBehaviour,IYellow
{
    [Header("Yellow")]
    [SerializeField] private float speed;
    [SerializeField] private float up;
    private Vector3 _minPosition;
    private Vector3 _maxPosition;
    private Vector3 target;

    private void Start()
    {
        _minPosition = transform.position;
        _maxPosition = transform.position;
        _maxPosition.y = _maxPosition.y + up;

        target = _maxPosition;
    }

    public Material Color
    {
        get
        {
            return gameObject.GetComponent<MeshRenderer>().material;
        }
        set
        {
            gameObject.GetComponent<MeshRenderer>().material = value;
        }
    }
    private void Update()
    {
        if(tag == "Yellow")
        {
            Move(_minPosition, _maxPosition);
        }
    }
    public void Move(Vector3 minPosition, Vector3 maxPosition)
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

        if (transform.position == maxPosition)
        {
            target = minPosition;
        }
        if (transform.position == minPosition)
        {
            target = maxPosition;
        }
    }

    public IEnumerator Movable()
    {
        throw new System.NotImplementedException();
    }
}
