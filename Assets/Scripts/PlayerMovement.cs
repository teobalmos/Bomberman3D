using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerMovement : MonoBehaviour
{   
    [SerializeField]
    public float playerSpeed;
    [SerializeField]
    public Sprite downIdleSprite;
    [SerializeField]
    public Sprite upIdleSprite;
    [SerializeField]
    public Sprite rightIdleSprite;
    [SerializeField]
    public Sprite upMoveSprite;
    [SerializeField]
    public Sprite rightMoveSprite;
    [SerializeField]
    public Sprite downMoveSprite;

    private Vector3 moveDirection = Vector3.zero;
    CharacterController characterController;
    SpriteRenderer sprite;
    private GameObject bomb;

    private enum Direction
    {
        Up,
        Right,
        Down,
        Left
    }
    private Direction lastDirection = Direction.Down;

    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        sprite = transform.GetChild(0).GetComponent<SpriteRenderer>() as SpriteRenderer;
        bomb = Resources.Load<GameObject>("Bomb") as GameObject;
    }

    private void movePlayer()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        moveDirection = new Vector3(moveHorizontal, 0.0f, moveVertical) * playerSpeed;

        moveDirection.y -= 200f * Time.deltaTime;

        characterController.Move(moveDirection * Time.deltaTime);

        if (Input.GetKey(KeyCode.UpArrow))
        {
            sprite.sprite = upMoveSprite;
            lastDirection = Direction.Up;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            sprite.sprite = rightMoveSprite;
            sprite.flipX = false;
            lastDirection = Direction.Right;
        }
        else if (Input.GetKey(KeyCode.DownArrow))
        {
            sprite.sprite = downMoveSprite;
            lastDirection = Direction.Down;
        }
        else if (Input.GetKey(KeyCode.LeftArrow))
        {
            sprite.sprite = rightMoveSprite;
            sprite.flipX = true;
            lastDirection = Direction.Left;
        }
        else if (!Input.anyKey)
        {
            if (lastDirection == Direction.Up)
            {
                sprite.sprite = upIdleSprite;
            }
            else if (lastDirection == Direction.Down)
            {
                sprite.sprite = downIdleSprite;
            }
            else if (lastDirection == Direction.Left)
            {
                sprite.sprite = rightIdleSprite;
                sprite.flipX = true;
            }
            else if (lastDirection == Direction.Right)
            {
                sprite.sprite = rightIdleSprite;
                sprite.flipX = false;
            }
        }
    }

    void Update()
    {
        movePlayer();

        if(Input.GetKeyDown(KeyCode.Space)){
            Vector3 position = characterController.GetComponent<Transform>().position;

            position.x = (float)Math.Truncate(position.x);
            position.z = (float)Math.Truncate(position.z);

            GameObject bombObj = Instantiate(bomb, position, Quaternion.identity) as GameObject;

            Destroy(bombObj, 2);

        }
    }
}
