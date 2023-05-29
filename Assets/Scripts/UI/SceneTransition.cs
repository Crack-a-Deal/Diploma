using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class SceneTransition : MonoBehaviour
{
    [SerializeField] private Material transitionMaterial;
    private string propertyName = "_Progress";

    //public static Action OnTransitionDone;

    private void Start()
    {
        transitionMaterial.SetFloat(propertyName, 0f);
    }
    public IEnumerator Transition(float transitionTime)
    {
        Debug.Log("Transition");
        float currentTime = 0;
        while (currentTime <= transitionTime)
        {
            currentTime += Time.deltaTime;
            transitionMaterial.SetFloat(propertyName, Mathf.Clamp01(currentTime/transitionTime));
            yield return null;
        }
        transitionMaterial.SetFloat(propertyName, 0f);
        //OnTransitionDone?.Invoke();
    }
}
