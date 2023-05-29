using System;
using System.Collections;
using UnityEngine;

public class Cube : Colorable
{
    [Header("Yellow")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float moveTime;
    [SerializeField] private float moveHeight;
    [SerializeField] private float latency;

    private bool isMoving = false;
    private Coroutine coroutine;
      

    private Vector3 startPosition;
    private void Start()
    {
        startPosition = transform.position;
    }

    private void Update()
    {
        if (tag == "Yellow" && Transparency == 1)
        {
            if(!isMoving)
                coroutine = StartCoroutine(MoveObject());
        }
        else
        {
            if(coroutine != null)
            {
                isMoving = false;
                StopAllCoroutines();
            }
        }
    }

    #region YELLOW
    private IEnumerator MoveObject()
    {
        isMoving = true;
        if (transform.position != startPosition)
        {
            yield return StartCoroutine(Move(transform.position, startPosition));
        }

        // поднимаем объект на указанную высоту
        Vector3 endPosition = startPosition + Vector3.up * moveHeight;
        yield return StartCoroutine(Move(startPosition, endPosition));

        // возвращаем объект в стартовую позицию
        yield return StartCoroutine(Move(endPosition, startPosition));

        isMoving = false;
    }
    IEnumerator Move(Vector3 from, Vector3 where)
    {
        float t = 0;
        while (t < 1)
        {
            t += moveSpeed * Time.deltaTime;
            transform.position = Vector3.Lerp(from, where, t);
            yield return null;
        }

        // устанавливает объект в точную  позицию
        transform.position = where;

        // ждем некоторое время в точке
        yield return new WaitForSeconds(latency);
    }
    #endregion

    public void SetTransparent(float transparent)
    {
        Material materia = GetComponent<MeshRenderer>().sharedMaterial;
        materia.SetFloat("_Transparent", transparent);
        OnTransparencyChanged?.Invoke();
        if (transparent == 1)
        {
            gameObject.layer = 6;
        }
        else
        {
            gameObject.layer = 4;
        }
    }
}
