using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

public class HumanCharacterController : ICharacterInputController
{
    private InputActionMap actionMap;

    public HumanCharacterController(InputActionMap map)
    {
        actionMap = map;
    }

    public Vector2 GetDirection()
    {   
        if (actionMap.name == "Controller")
        {
            Debug.Log($"values in get direction: {actionMap["Move"].ReadValue<Vector2>()}");
        }
        return actionMap["Move"].ReadValue<Vector2>();
    }

    public bool GetFire()
    {
        Debug.Log($"Fire: {actionMap["Fire"].ReadValue<float>()}");
        return actionMap["Fire"].ReadValue<float>() == 1f?true:false;
    }
}
