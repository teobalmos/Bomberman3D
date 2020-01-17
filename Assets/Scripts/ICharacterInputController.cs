using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ICharacterInputController{
    void GetInput(ref CharacterInput input);
} 

public struct CharacterInput{
    public float horizontal, vertical; // [-1, 1] Input Get Axis
    public bool bombDrop;
}