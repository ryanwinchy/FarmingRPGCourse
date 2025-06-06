using UnityEngine;

public class Player : SingletonMonobehaviour<Player>   //Inherits from singleton, so runs the singleton monobehaviour Awake method. Just one instance.
{                                //Player is a singleton, only one, makes easier to reference and save. And only 1 player.

    //Movement parameters.

    float xInput, yInput;
    bool isCarrying = false;
    bool isIdle;

    bool isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown;
    bool isRunning;
    bool isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown;
    bool isSwingingToolLeft, isSwingingToolRight, isSwingingToolUp, isSwingingToolDown;
    bool isWalking;
    bool isPickingLeft, isPickingRight, isPickingUp, isPickingDown;
    ToolEffect toolEffect = ToolEffect.None;


    private Rigidbody2D rigidBody2D;

#pragma warning disable 414      //Disable console warning for unused player direction.
    Direction playerDirection;   //Will save current direction player is facing.
#pragma warning restore 414
    float movementSpeed;

    public bool PlayerInputIsDisabled { get; set; } = false;

    AnimationParameters animationParameters;


    protected override void Awake()
    {
        base.Awake();            //Execute the parent awake, making it singleton.

        rigidBody2D = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        #region Player Input

        ResetAnimationTriggers();

        PlayerMovementInput();

        PlayerWalkInput();

        //Pass in movement params to any listeners for player movement input.
        EventHandler.CallMovementEvent(xInput, yInput, isWalking, isRunning, isIdle, isCarrying, toolEffect, isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown, isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown, isPickingRight, isPickingLeft, isPickingUp, isPickingDown, isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown, false, false, false, false);

        #endregion
    }
    
    private void FixedUpdate()    //Good to capture input in update and apply in fixed update.
    {
        PlayerMovement();
    }

    private void PlayerMovement()
    {
        Vector2 move = new Vector2(xInput * movementSpeed * Time.deltaTime, yInput * movementSpeed * Time.deltaTime);

        rigidBody2D.MovePosition(rigidBody2D.position + move);
    }

    private void ResetAnimationTriggers()
    {
        isPickingLeft = false;
        isPickingRight = false;
        isPickingUp = false;
        isPickingDown = false;
        isUsingToolLeft = false;
        isUsingToolRight = false;
        isUsingToolUp = false;
        isUsingToolDown = false;
        isLiftingToolLeft = false;
        isLiftingToolRight = false;
        isLiftingToolUp = false;
        isLiftingToolDown = false;
        isSwingingToolLeft = false;
        isSwingingToolRight = false;
        isSwingingToolUp = false;
        isSwingingToolDown = false;
        toolEffect = ToolEffect.None;


    }

    private void PlayerMovementInput()
    {
        yInput = Input.GetAxisRaw("Vertical"); //These return 1 if right, -1 if left, 0 if not pressed.
        xInput = Input.GetAxisRaw("Horizontal");

        if (xInput != 0 && yInput != 0)   //Pythagoras for diagonal so speed is same if double input in 2 dirs.
        {
            xInput = xInput * 0.71f;
            yInput = yInput * 0.71f;
        }

        if (xInput != 0 || yInput != 0)
        {
            isRunning = true;
            isWalking = false;
            isIdle = false;
            movementSpeed = Settings.RUNNING_SPEED;



            //Capture player dir for save game.

            if (xInput < 0)
            {
                playerDirection = Direction.Left;
            }
            else if (xInput > 0)
            {
                playerDirection = Direction.Right;
            }
            else if (yInput < 0)
            {
                playerDirection = Direction.Down;
            }
            else if (yInput > 0)
            {
                playerDirection = Direction.Up;
            }
        }
        else if (xInput == 0 && yInput == 0)
        {
            isIdle = true;
            isWalking = false;
            isRunning = false;

        }


    }

    private void PlayerWalkInput()
    {
        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            isRunning = false;
            isWalking = true;
            isIdle = false;
            movementSpeed = Settings.WALKING_SPEED;
        }
        else
        {
            isRunning = true;
            isWalking = false;
            isIdle = false;
            movementSpeed = Settings.RUNNING_SPEED;
        }
    }


   









}
