using System.Collections.Generic;
using System;

public delegate void MovementDelegate(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle, bool isCarrying, ToolEffect toolEffect, bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown,
bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown,
bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown,
bool idleUp, bool idleDown, bool idleLeft, bool idleRight);
//delegate just holds a reference to a method. Have to use delegate as so many params.

public static class EventHandler       //We use this globally to handle all our events. THE REASON IS BECAUSE WE HAVE PERSISTENT SCENE FOR PLAYER AND TIME ETC... AND MANY SCENES.SO OTHER SCENES CANT REF TIME MANAGER FOR EG THATS PERSISTENT. SO WE HAVE EVENTS ALL IN ONE CENTRAL SCENE INDEPENDENT PLACE. BUT PROBS BETTER WAY TO DO. CAN THINK ABOUT IT.
{

    //Inventory updated event (sub to this)
    public static event Action<InventoryLocation, List<InventoryItem>> OnInventoryUpdatedEvent;
    //I WOULD NOT DO IT LIKE THIS. IT'S STUPID. SHOULD BE ON THE INSTANCE WITH THE INVENTORY FIRING THE EVENT. ALL EVENTS CENTRAL IS JUST WEIRD.
    //THATS WHAT IT IS FOR. INSTEAD OF FIRING STATICS WITH NO SENDER THEN SAYING WHO THE SENDER IS. STUPID.


    //Inventory updated event call for publishers.
    public static void CallInventoryUpdatedEvent(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
    {
        OnInventoryUpdatedEvent?.Invoke(inventoryLocation, inventoryList);   //if not null means there are listeners, so fire the event with all the params.
    }







    //Movement event (sub to this)
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






    //Time system events.

    //advance game minute.
    //sub to this
    public static event Action<int, Season, int, string, int, int, int> OnAdvanceGameMinuteEvent;



    //advance game minute event call for publishers.
    public static void CallAdvanceGameMinuteEvent(int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek, int gameHour, int gameMinute, int gameSecond)
    {
        OnAdvanceGameMinuteEvent?.Invoke(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);   //if not null means there are listeners, so fire the event with all the params.
    }




    //advance game hour.
    public static event Action<int, Season, int, string, int, int, int> OnAdvanceGameHourEvent;



    //advance game hour event call for publishers.
    public static void CallAdvanceGameHourEvent(int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek, int gameHour, int gameMinute, int gameSecond)
    {
        OnAdvanceGameHourEvent?.Invoke(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);   //if not null means there are listeners, so fire the event with all the params.
    }



    //advance game day.
    public static event Action<int, Season, int, string, int, int, int> OnAdvanceGameDayEvent;



    //advance game minute event call for publishers.
    public static void CallAdvanceGameDayEvent(int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek, int gameHour, int gameMinute, int gameSecond)
    {
        OnAdvanceGameDayEvent?.Invoke(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);   //if not null means there are listeners, so fire the event with all the params.
    }


    //advance game season.
    public static event Action<int, Season, int, string, int, int, int> OnAdvanceGameSeasonEvent;



    //advance game minute event call for publishers.
    public static void CallAdvanceGameSeasonEvent(int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek, int gameHour, int gameMinute, int gameSecond)
    {
        OnAdvanceGameSeasonEvent?.Invoke(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);   //if not null means there are listeners, so fire the event with all the params.
    }


    //advance game year.
    public static event Action<int, Season, int, string, int, int, int> OnAdvanceGameYearEvent;



    //advance game minute event call for publishers.
    public static void CallAdvanceGameYearEvent(int gameYear, Season gameSeason, int gameDay, string gameDayOfWeek, int gameHour, int gameMinute, int gameSecond)
    {
        OnAdvanceGameYearEvent?.Invoke(gameYear, gameSeason, gameDay, gameDayOfWeek, gameHour, gameMinute, gameSecond);   //if not null means there are listeners, so fire the event with all the params.
    }


}
