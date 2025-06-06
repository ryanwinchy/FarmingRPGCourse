public delegate void MovementDelegate(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle, bool isCarrying, ToolEffect toolEffect, bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown,
bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown,
bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown,
bool idleUp, bool idleDown, bool idleLeft, bool idleRight);
//delegate just holds a reference to a method have to use delegate as so many params.

public static class EventHandler       //We use this globally to handle all our events.
{

    //Movement event.
    public static event MovementDelegate OnMovementEvent;   //Subscribers can subscribe to this event and get all the movement animation info.


    // public static void CallMovementEvent(AnimationParameters animationParameters)
    // {

    //     OnMovementEvent?.Invoke(null, animationParameters);            //If not null means there are listeners, so fire the event with all the params. Null sender as is static no monobheaviour.

    // }


    //Movement event call for publishers.
        public static void CallMovementEvent(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle, bool isCarrying, ToolEffect toolEffect, bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
    bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown,
    bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown,
    bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown,
    bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
        {
            if (OnMovementEvent != null)  //If not null means there are listeners, so fire the event with all the params.
            {
                OnMovementEvent(inputX, inputY, isWalking, isRunning, isIdle, isCarrying, toolEffect, isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown, isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown, isPickingRight, isPickingLeft, isPickingUp, isPickingDown, isSwingingToolRight, isSwingingToolLeft, isSwingingToolUp, isSwingingToolDown, idleUp, idleDown, idleLeft, idleRight);
            }
        }


}
