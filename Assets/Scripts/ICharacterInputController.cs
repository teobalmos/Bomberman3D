using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public interface ICharacterInputController
{
    Vector2 GetDirection();
    bool GetFire();
} 

public struct CharacterInput{
    public float horizontal, vertical; // [-1, 1] Input Get Axis
    public bool bombDrop;
}