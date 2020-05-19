using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AICharacterController : ICharacterInputController {
    public void OnMove(ref CharacterInput input){
        input.horizontal = Random.Range(-1f, 1f);
        input.vertical = Random.Range(-1f, 1f);

        input.bombDrop = Random.Range(0, 2) == 1 ? true : false;
    }

    public Vector2 GetDirection()
    {
        return Vector2.down;
    }

    public bool GetFire()
    {
        return false;
    }
}
