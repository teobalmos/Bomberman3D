using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputMovement : MonoBehaviour
{
    [SerializeField] public GameObject bomb;
    
    private Vector2 direction;
    private const float moveSpeed = 10f;
    private Animator animator;
    private ICharacterInputController _iCharacterInputController;
    private InputActionAsset actionAsset;
    private PlayerInput playerInput;
    private float bombLifetime = 2f;

    private void Start()
    {
        var playerInput = GetComponent<PlayerInput>();
        _iCharacterInputController = new HumanCharacterController(playerInput.currentActionMap);
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        Move();
        var fired = _iCharacterInputController.GetFire();
        if (fired)
        {
            PlaceBomb();
        }
    }

    private void Move()
    {
        var moveDirection = new Vector3(0f,0f,0f);
        
        direction = _iCharacterInputController.GetDirection();
        if (direction != Vector2.zero)
        {
            moveDirection = new Vector3(direction.x, 0, direction.y);

            var movement = moveDirection * moveSpeed * Time.deltaTime;
            transform.Translate(movement, Space.World);
        }
        
        var speed = new Vector2(direction.x, direction.y).sqrMagnitude;
        animator.SetFloat("Blend", speed);
        
        if (speed > 0f)
        {
            transform.rotation = Quaternion.LookRotation(moveDirection);
        }
    }
    
    private void PlaceBomb()
    {
        var position = transform.position;
        Debug.Log($"positions: {position}");

        position.x = (float)Math.Round(position.x);
        position.z = (float)Math.Round(position.z);
        position.y = 0.5f;

        var colliders = Physics.OverlapSphere(position, 0.4f, 9);

        var isMap = false;

        foreach(var collider in colliders){
            if(collider.gameObject.tag == "Map" || collider.gameObject.tag == "Crate"){
                isMap = true;
            }
        }

        if(!isMap){
            Debug.Log(position);

            var bombObj = Instantiate(bomb, position, Quaternion.identity);
            
            Destroy(bombObj, bombLifetime);
            
        }
    }
}
