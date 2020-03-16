﻿using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{   
    [SerializeField]
    public float playerSpeed;

    [SerializeField] public GameObject bomb;

    private Vector3 moveDirection = Vector3.zero;
    CharacterController characterController;
    private Animator anim;

    private ICharacterInputController _iCharacterInputController;

    private void Start()
    {
        _iCharacterInputController = new HumanCharacterController();
        // _iCharacterInputController = new AICharacterController();
        anim = this.GetComponent<Animator> ();
        characterController = GetComponent<CharacterController>();
    }

    private void movePlayer(in CharacterInput input)
    {
        var moveHorizontal = input.horizontal;
        var moveVertical = input.vertical;

        moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical);
        
        var Speed = new Vector2(moveHorizontal, moveVertical).sqrMagnitude;
        anim.SetFloat("Blend", Speed );

        if(Speed > 0f){
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }

        moveDirection.y -= 200f * Time.deltaTime;
        characterController.Move(moveDirection * playerSpeed * Time.deltaTime);

    }

    private void placeBomb(){
        var position = characterController.GetComponent<Transform>().position;

        position.x = (float)Math.Round(position.x);
        position.z = (float)Math.Round(position.z);
        position.y = 0.5f;

        var colliders = Physics.OverlapSphere(position, 0.4f, 9);

        var isMap = false;

        foreach(var collider in colliders){
            Debug.Log(collider.gameObject.tag);
            if(collider.gameObject.tag == "Map" || collider.gameObject.tag == "Crate"){
                isMap = true;
            }
        }

        if(!isMap){
            Debug.Log(position);
            
            Instantiate(bomb, position, Quaternion.identity);
        }
    }

    private void Update()
    {
        var input = new CharacterInput();
        _iCharacterInputController.GetInput(ref input);

        movePlayer(in input);

        var bombFound = GameObject.FindGameObjectsWithTag("Bomb");

        if (bombFound != null && bombFound.Length > 0)
        {
            Debug.Log("Collision ignored: " + Physics.GetIgnoreCollision(GetComponent<Collider>(), bombFound[0].GetComponent<Collider>()));

        }
        if (input.bombDrop){
            placeBomb();
        }
    }
}
