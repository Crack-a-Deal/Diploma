using System;
using System.Collections;
using UnityEngine;

public class SwitchableCube : Colorable
{
    [SerializeField] private float time;

    private void Start()
    {
        StartCoroutine(Switchable());
    }

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
    private IEnumerator Switchable()
    {
        while (true)
        {
            SetTransparent(1f);
            yield return new WaitForSeconds(time);
            SetTransparent(0.5f);
            yield return new WaitForSeconds(time);
        }
    }
}
