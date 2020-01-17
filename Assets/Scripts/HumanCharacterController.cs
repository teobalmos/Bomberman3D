using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HumanCharacterController : ICharacterInputController{
    public void GetInput(ref CharacterInput input){
        var moveHorizontal = Input.GetAxis("Horizontal");
        var moveVertical = Input.GetAxis("Vertical");

        input.horizontal = moveHorizontal;
        input.vertical = moveVertical;

        input.bombDrop = Input.GetKeyDown(KeyCode.Space);
    }
}
