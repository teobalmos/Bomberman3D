using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{   
    [SerializeField]
    public float playerSpeed;

    private Vector3 moveDirection = Vector3.zero;
    CharacterController characterController;
    private GameObject bomb;
    private Animator anim;

    private ICharacterInputController _iCharachterInputController;

    void Start()
    {
        _iCharachterInputController = new HumanCharacterController();
        // _iCharachterInputController = new AICharacterController();
        anim = this.GetComponent<Animator> ();
        characterController = GetComponent<CharacterController>();
        bomb = Resources.Load<GameObject>("Bomb") as GameObject;
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
        Vector3 position = characterController.GetComponent<Transform>().position;

        position.x = (float)Math.Round(position.x);
        position.z = (float)Math.Round(position.z);
        position.y = 0.5f;

        Collider[] colliders = Physics.OverlapSphere(position, 0.4f, 9);

        var isMap = false;

        foreach(Collider collider in colliders){
            Debug.Log(collider.gameObject.tag);
            if(collider.gameObject.tag == "Map" || collider.gameObject.tag == "Crate"){
                isMap = true;
            }
        }

        if(!isMap){
            Debug.Log(position);
            
            var bombObj = Instantiate(bomb, position, Quaternion.identity) as GameObject;

            //            Physics.IgnoreLayerCollision(9, 9, true);

            //Physics.IgnoreCollision(GetComponent<Collider>(), bombObj.GetComponent<BoxCollider>(), true);
            //Destroy(bombObj, 2);
        }
    }

    void Update()
    {
        var input = new CharacterInput();
        _iCharachterInputController.GetInput(ref input);

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
