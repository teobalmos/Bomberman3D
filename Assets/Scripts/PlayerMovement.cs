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

    void Start()
    {
        anim = this.GetComponent<Animator> ();
        characterController = GetComponent<CharacterController>();
        bomb = Resources.Load<GameObject>("Bomb") as GameObject;
    }

    private void movePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

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

            Debug.Log(colliders.GetLength(0));

            bool isMap = false;

            foreach(Collider collider in colliders){
                Debug.Log(collider.gameObject.tag);
                if(collider.gameObject.tag == "Map"){
                    isMap = true;
                }
            }

            if(!isMap){
                Debug.Log(position);
                GameObject bombObj = Instantiate(bomb, position, Quaternion.identity) as GameObject;

                Destroy(bombObj, 2);
            }
    }

    void Update()
    {
        movePlayer();

        if(Input.GetKeyDown(KeyCode.Space)){
            placeBomb();
        }
    }

    void OnCollisionEnter(Collision collision){
        if(collision.collider.tag == "Bomb"){
            Debug.Log("Bomb");
        }
    }
}
