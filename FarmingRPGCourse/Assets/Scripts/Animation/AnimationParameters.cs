using System;
using UnityEngine;

[Serializable]
public class AnimationParameters       //Much cleaner than using so many anim params passed around.
{
    // Player Animation Parameters
    public float xInput, yInput;
    public bool isWalking, isRunning;
    public ToolEffect toolEffect;
    public bool isUsingToolRight, isUsingToolLeft, isUsingToolUp, isUsingToolDown;
    public bool isLiftingToolRight, isLiftingToolLeft, isLiftingToolUp, isLiftingToolDown;
    public bool isPickingLeft, isPickingRight, isPickingUp, isPickingDown;
    public bool isSwingingToolLeft, isSwingingToolRight, isSwingingToolUp, isSwingingToolDown;




    // Shared Animation Parameters (Player, and NPC)

    public bool isIdleLeft, isIdleRight, isIdleUp, isIdleDown;




}
