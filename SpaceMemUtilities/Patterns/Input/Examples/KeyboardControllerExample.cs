using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using SpaceMem.Patterns.Input;

public class KeyMappingsController : MonoBehaviour
{
    //Andrea: check if this is too expensive
    [SerializeField]
    private ShortcutKeys _activateFunctionExample = new ShortcutKeys("Activate function", 2)
    {
        Key0 = KeyCode.S,
        Key1 = KeyCode.X,
        UseModifiers = false,
        UseMouseButtons = false
    };


    public ShortcutKeys ActivateFunctionExample { get { return _activateFunctionExample; } }

    void Update()
    {
        if (Input.GetKeyDown(_activateFunctionExample.Key0))
        {
            StartFunctionExample();
        }
        else
        if (Input.GetKeyDown(_activateFunctionExample.Key1))
        {
            StopFunctionExample();
        }
    }

    private void StartFunctionExample()
    { 
        Debug.Log("Start function example");
    }
    private void StopFunctionExample()
    {
        Debug.Log("Stop function example");
    }
}