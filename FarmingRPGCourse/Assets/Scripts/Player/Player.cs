using UnityEngine;
using System.Collections.Generic;

public class Player : SingletonMonobehaviour<Player>   //Inherits from singleton, so runs the singleton monobehaviour Awake method. Just one instance.
{                                //Player is a singleton, only one, makes easier to reference and save. And only 1 player.

    
    AnimationOverrides animationOverrides;

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


    Camera mainCamera;
    private Rigidbody2D rigidBody2D;

#pragma warning disable 414      //Disable console warning for unused player direction.
    Direction playerDirection;   //Will save current direction player is facing.
#pragma warning restore 414

    List<CharacterAttribute> characterAttributeCustomisationList;   //things we want to swap out anims for. like arms for carrying.
    float movementSpeed;

    [Tooltip("Populate with prefab with the equipped item sprite renderer.")]  //So we can put equipped item like pumpkin above head on player.
    [SerializeField] SpriteRenderer equippedItemSpriteRenderer = null;

    //Player attributes that can be swapped.
    CharacterAttribute armsCharacterAttribute;
    CharacterAttribute toolCharacterAttribute;

    public bool PlayerInputIsDisabled { get; set; } = false;

    AnimationParameters animationParameters;


    protected override void Awake()
    {
        base.Awake();            //Execute the parent awake, making it singleton.

        rigidBody2D = GetComponent<Rigidbody2D>();

        animationOverrides = GetComponentInChildren<AnimationOverrides>();    //this is our script to override animators based on things like carrying, it goes on same object as animators.

        //Initialise swappable character attributes.
        armsCharacterAttribute = new CharacterAttribute(CharacterPartAnimator.Arms, PartVariantColour.None, PartVariantType.None);

        characterAttributeCustomisationList = new List<CharacterAttribute>();

        mainCamera = Camera.main;    //This is intensive so best to cache.
    }

    void Update()
    {
        #region Player Input

        if (!PlayerInputIsDisabled)        //so cant move while dragging inventory items and such.
        {
            ResetAnimationTriggers();

            PlayerMovementInput();

            PlayerWalkInput();

            PlayerTestInput();

            //Pass in movement params to any listeners for player movement input.
            EventHandler.CallMovementEvent(xInput, yInput, isWalking, isRunning, isIdle, isCarrying, toolEffect, isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown, isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown, isPickingRight, isPickingLeft, isPickingUp, isPickingDown, isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown, false, false, false, false);

        }


        #endregion
    }
    
    private void FixedUpdate()    //Good to capture input in update and apply in fixed update. I THINK THIS IS WRONG - MAKING IT HAPPEN NEXT FRAME IS WEIRD, A TINY JITTER.
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


    /// <summary>
    /// Temp routine for test input for advance time.
    /// </summary>
    void PlayerTestInput()
    {
        if (Input.GetKeyDown(KeyCode.T))
        {
            TimeManager.Instance.TestAdvanceGameMinute();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            TimeManager.Instance.TestAdvanceGameDay();
        }
    }

    private void ResetMovement()
    {
        xInput = 0f;
        yInput = 0f;
        isWalking = false;
        isRunning = false;
        isIdle = true;

    }

    public void DisablePlayerInputAndResetMovement()
    {
        DisablePlayerInput();
        ResetMovement();

        //Send event to any listeners for player movement input (same as in update).
        EventHandler.CallMovementEvent(xInput, yInput, isWalking, isRunning, isIdle, isCarrying, toolEffect, isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown, isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown, isPickingRight, isPickingLeft, isPickingUp, isPickingDown, isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown, false, false, false, false);

    }

    public void DisablePlayerInput() => PlayerInputIsDisabled = true;
    public void EnablePlayerInput() => PlayerInputIsDisabled = false;


    public void ClearCarriedItem()
    {
        equippedItemSpriteRenderer.sprite = null;
        equippedItemSpriteRenderer.color = new Color(1f, 1f, 1f, 0f);

        //Apple base char arms animation. SO we overrode to get arms carrying animator, now we want base.
        armsCharacterAttribute.partVariantType = PartVariantType.None;

        characterAttributeCustomisationList.Clear();
        characterAttributeCustomisationList.Add(armsCharacterAttribute);

        animationOverrides.ApplyCharacterCustomisationParameters(characterAttributeCustomisationList);

        isCarrying = false;
    }

    public void ShowCarriedItem(int itemCode)
    {
        ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(itemCode);

        if (itemDetails != null)
        {
            equippedItemSpriteRenderer.sprite = itemDetails.itemSprite;    //set sprite to item passed in and full opacity.
            equippedItemSpriteRenderer.color = new Color(1f, 1f, 1f, 1f);

            //Apply 'carry' character arms customisation by overriding animator.
            armsCharacterAttribute.partVariantType = PartVariantType.Carry;

            characterAttributeCustomisationList.Clear();
            characterAttributeCustomisationList.Add(armsCharacterAttribute);

            animationOverrides.ApplyCharacterCustomisationParameters(characterAttributeCustomisationList);   //applies carrying to animator.

            isCarrying = true;
        }
    }



    public Vector3 GetPlayerViewportPosition()   //Gets camera position (viewport) of player. Viewport is (0,0) for bottom left, (1,1) for top right.
    {
        return mainCamera.WorldToViewportPoint(transform.position);
    }









}
