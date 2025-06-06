using UnityEngine;

public class PlayerAnimationTest : MonoBehaviour
{

    // [SerializeField] public AnimationParameters playerAnimationParameters = new AnimationParameters();   //Creates new instance of all our anim parameters like using tool, lifting tool, etc.

    // void Update()
    // {
    //     EventHandler.CallMovementEvent(playerAnimationParameters);
    // } 

    public float inputX;
    public float inputY;
    public bool isWalking;
    public bool isRunning;
    public bool isIdle;
    public bool isCarrying;
    public ToolEffect toolEffect;
    public bool isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown;
    public bool isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown;
    public bool isPickingLeft, isPickingRight, isPickingUp, isPickingDown;
    public bool isSwingingToolLeft, isSwingingToolRight, isSwingingToolUp, isSwingingToolDown;
    public bool idleUp, idleDown, idleLeft, idleRight;

    void Update()
    {
        EventHandler.CallMovementEvent(inputX, inputY, isWalking, isRunning, isIdle, isCarrying, toolEffect, isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown, isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown, isPickingRight, isPickingLeft, isPickingUp, isPickingDown, isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown, idleUp, idleDown, idleLeft, idleRight);
    }
    
    
    
    

}
