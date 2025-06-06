using UnityEngine;

public class MovementAnimationParamaterControl : MonoBehaviour        //Goes on each animator component to tell it what to play.
{
    
    Animator animator;

    void Awake()
    {
        animator = GetComponent<Animator>();
    }

    void OnEnable()
    {
        EventHandler.OnMovementEvent += OnMovementEvent_SetAnimationParameters;
    }

    void OnDisable()
    {
        EventHandler.OnMovementEvent -= OnMovementEvent_SetAnimationParameters;
    }


   


    // //Receive vals from movement event, sets it to the animator. As all player layers need to display the anim. Many things animate to one input.
    // void OnMovementEvent_SetAnimationParameters(object sender, AnimationParameters animationParameters)
    // {
    //     animator.SetFloat(Settings.xInput, animationParameters.xInput);    //Sets float thats passed in to the inputX anim value.
    //     animator.SetFloat(Settings.yInput, animationParameters.yInput);
    //     animator.SetBool(Settings.isWalking, animationParameters.isWalking);
    //     animator.SetBool(Settings.isRunning, animationParameters.isRunning);

    //     animator.SetInteger(Settings.toolEffect, (int)animationParameters.toolEffect);

    //     if (animationParameters.isUsingToolRight)
    //         animator.SetTrigger(Settings.isUsingToolRight);
    //     if (animationParameters.isUsingToolLeft)
    //         animator.SetTrigger(Settings.isUsingToolLeft);
    //     if (animationParameters.isUsingToolUp)
    //         animator.SetTrigger(Settings.isUsingToolUp);
    //     if (animationParameters.isUsingToolDown)
    //         animator.SetTrigger(Settings.isUsingToolDown);

    //     if (animationParameters.isLiftingToolRight)
    //         animator.SetTrigger(Settings.isLiftingToolRight);
    //     if (animationParameters.isLiftingToolLeft)
    //         animator.SetTrigger(Settings.isLiftingToolLeft);
    //     if (animationParameters.isLiftingToolUp)
    //         animator.SetTrigger(Settings.isLiftingToolUp);
    //     if (animationParameters.isLiftingToolDown)
    //         animator.SetTrigger(Settings.isLiftingToolDown);

    //     if (animationParameters.isPickingRight)
    //         animator.SetTrigger(Settings.isPickingRight);
    //     if (animationParameters.isPickingLeft)
    //         animator.SetTrigger(Settings.isPickingLeft);
    //     if (animationParameters.isPickingUp)
    //         animator.SetTrigger(Settings.isPickingUp);
    //     if (animationParameters.isPickingDown)
    //         animator.SetTrigger(Settings.isPickingDown);

    //     if (animationParameters.isSwingingToolRight)
    //         animator.SetTrigger(Settings.isSwingingToolRight);
    //     if (animationParameters.isSwingingToolLeft)
    //         animator.SetTrigger(Settings.isSwingingToolLeft);
    //     if (animationParameters.isSwingingToolUp)
    //         animator.SetTrigger(Settings.isSwingingToolUp);
    //     if (animationParameters.isSwingingToolDown)
    //         animator.SetTrigger(Settings.isSwingingToolDown);

    //     if (animationParameters.isIdleUp)
    //         animator.SetTrigger(Settings.idleUp);
    //     if (animationParameters.isIdleDown)
    //         animator.SetTrigger(Settings.idleDown);
    //     if (animationParameters.isIdleLeft)
    //         animator.SetTrigger(Settings.idleLeft);
    //     if (animationParameters.isIdleRight)
    //         animator.SetTrigger(Settings.idleRight);




    // }

    //Receive vals from movement event, sets it to the animator. As all player layers need to display the anim. Many things animate to one input.
        void OnMovementEvent_SetAnimationParameters(float inputX, float inputY, bool isWalking, bool isRunning, bool isIdle, bool isCarrying, ToolEffect toolEffect, bool isUsingToolRight, bool isUsingToolLeft, bool isUsingToolUp, bool isUsingToolDown,
    bool isLiftingToolRight, bool isLiftingToolLeft, bool isLiftingToolUp, bool isLiftingToolDown,
    bool isPickingRight, bool isPickingLeft, bool isPickingUp, bool isPickingDown,
    bool isSwingingToolRight, bool isSwingingToolLeft, bool isSwingingToolUp, bool isSwingingToolDown,
    bool idleUp, bool idleDown, bool idleLeft, bool idleRight)
        {
            animator.SetFloat(Settings.xInput, inputX);    //Sets float thats passed in to the inputX anim value.
            animator.SetFloat(Settings.yInput, inputY);
            animator.SetBool(Settings.isWalking, isWalking);
            animator.SetBool(Settings.isRunning, isRunning);

            animator.SetInteger(Settings.toolEffect, (int)toolEffect);

            if (isUsingToolRight)
                animator.SetTrigger(Settings.isUsingToolRight);
            if (isUsingToolLeft)
                animator.SetTrigger(Settings.isUsingToolLeft);
            if (isUsingToolUp)
                animator.SetTrigger(Settings.isUsingToolUp);
            if (isUsingToolDown)
                animator.SetTrigger(Settings.isUsingToolDown);

            if (isLiftingToolRight)
                animator.SetTrigger(Settings.isLiftingToolRight);
            if (isLiftingToolLeft)
                animator.SetTrigger(Settings.isLiftingToolLeft);
            if (isLiftingToolUp)
                animator.SetTrigger(Settings.isLiftingToolUp);
            if (isLiftingToolDown)
                animator.SetTrigger(Settings.isLiftingToolDown);

            if (isPickingRight)
                animator.SetTrigger(Settings.isPickingRight);
            if (isPickingLeft)
                animator.SetTrigger(Settings.isPickingLeft);
            if (isPickingUp)
                animator.SetTrigger(Settings.isPickingUp);
            if (isPickingDown)
                animator.SetTrigger(Settings.isPickingDown);

            if (isSwingingToolRight)
                animator.SetTrigger(Settings.isSwingingToolRight);
            if (isSwingingToolLeft)
                animator.SetTrigger(Settings.isSwingingToolLeft);
            if (isSwingingToolUp)
                animator.SetTrigger(Settings.isSwingingToolUp);
            if (isSwingingToolDown)
                animator.SetTrigger(Settings.isSwingingToolDown);

            if (idleUp)
                animator.SetTrigger(Settings.idleUp);
            if (idleDown)
                animator.SetTrigger(Settings.idleDown);
            if (idleLeft)
                animator.SetTrigger(Settings.idleLeft);
            if (idleRight)
                animator.SetTrigger(Settings.idleRight);




        }


    private void AnimationEventPlayFootstepSound()
    {

    }
}
