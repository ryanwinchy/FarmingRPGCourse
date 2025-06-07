using UnityEngine;

public static class Settings     //This would be better as singleton instead of static, as then could edit settings in editor / runtime. Or a scriptable object.
{

    //Obscuring item fade settings.
    public const float FADE_IN_SECONDS = 0.25f;
    public const float FADE_OUT_SECONDS = 0.35f;
    public const float TARGET_ALPHA = 0.45f;

    //Player movement settings.
    public const float RUNNING_SPEED = 5.333f;
    public const float WALKING_SPEED = 2.666f;

    //Inventory
    public static int playerInitialInventoryCapacity = 24;  //starting.
    public static int playerMaximumInventoryCapacity = 48;      //if upgrade.

    //Player animation parameters, cleaner and kinder on memory than using strings.
    public static int xInput;
    public static int yInput;
    public static int isWalking;
    public static int isRunning;
    public static int toolEffect;
    public static int isUsingToolRight;
    public static int isUsingToolLeft;
    public static int isUsingToolUp;
    public static int isUsingToolDown;
    public static int isLiftingToolRight;
    public static int isLiftingToolLeft;
    public static int isLiftingToolUp;
    public static int isLiftingToolDown;
    public static int isSwingingToolRight;
    public static int isSwingingToolLeft;
    public static int isSwingingToolUp;
    public static int isSwingingToolDown;
    public static int isPickingRight;
    public static int isPickingLeft;
    public static int isPickingUp;
    public static int isPickingDown;

    //Shared animation parameters (player and NPCs)
    public static int idleUp;
    public static int idleDown;
    public static int idleLeft;
    public static int idleRight;

    //Tools.
    public const string HOEING_TOOL = "Hoe";
    public const string CHOPPING_TOOL = "Axe";
    public const string BREAKING_TOOL = "Pickaxe";
    public const string REAPING_TOOL = "Scythe";
    public const string WATERING_TOOL = "Watering Can";
    public const string COLLECTING_TOOL = "Basket";
    



    //constructor to set up class.
    static Settings()
    {

        //Player only parameters.
        xInput = Animator.StringToHash("xInput");    //Converts the anim string to an int hash, so we store that instead of the string.
        yInput = Animator.StringToHash("yInput");      //static method on animator class.
        isWalking = Animator.StringToHash("isWalking");
        isRunning = Animator.StringToHash("isRunning");
        toolEffect = Animator.StringToHash("toolEffect");
        isUsingToolRight = Animator.StringToHash("isUsingToolRight");
        isUsingToolLeft = Animator.StringToHash("isUsingToolLeft");
        isUsingToolUp = Animator.StringToHash("isUsingToolUp");
        isUsingToolDown = Animator.StringToHash("isUsingToolDown");
        isLiftingToolRight = Animator.StringToHash("isLiftingToolRight");
        isLiftingToolLeft = Animator.StringToHash("isLiftingToolLeft");
        isLiftingToolUp = Animator.StringToHash("isLiftingToolUp");
        isLiftingToolDown = Animator.StringToHash("isLiftingToolDown");
        isSwingingToolRight = Animator.StringToHash("isSwingingToolRight");
        isSwingingToolLeft = Animator.StringToHash("isSwingingToolLeft");
        isSwingingToolUp = Animator.StringToHash("isSwingingToolUp");
        isSwingingToolDown = Animator.StringToHash("isSwingingToolDown");
        isPickingRight = Animator.StringToHash("isPickingRight");
        isPickingLeft = Animator.StringToHash("isPickingLeft");
        isPickingUp = Animator.StringToHash("isPickingUp");
        isPickingDown = Animator.StringToHash("isPickingDown");

        //Shared parameters (player and NPCs)
        idleUp = Animator.StringToHash("idleUp");
        idleDown = Animator.StringToHash("idleDown");
        idleLeft = Animator.StringToHash("idleLeft");
        idleRight = Animator.StringToHash("idleRight");


    }



}
