public enum AnimationName
{
    IdleDown,
    IdleUp,
    IdleRight,
    IdleLeft,
    WalkDown,
    WalkUp,
    WalkRight,
    WalkLeft,
    RunUp,
    RunDown,
    RunRight,
    RunLeft,
    UseToolUp,
    UseToolDown,
    UseToolRight,
    UseToolLeft,
    SwingToolUp,
    SwingToolDown,
    SwingToolRight,
    SwingToolLeft,
    LiftToolUp,
    LiftToolDown,
    LiftToolRight,
    LiftToolLeft,
    HoldToolUp,
    HoldToolDown,
    HoldToolRight,
    HoldToolLeft,
    PickUp,
    PickRight,
    PickLeft,
    PickDown,
    Count

}

public enum CharacterPartAnimator
{
    Body,
    Arms,
    Hair,
    Tool,
    Hat,
    Count
}

public enum PartVariantColour
{
    None,
    Count
}

public enum PartVariantType    //as anims vary based on what we carry. We override the animation.
{
    None,
    Carry,
    Hoe,
    Pickaxe,
    Axe,
    Scythe,
    WateringCan,
    Count
}

public enum InventoryLocation
{
    Player,
    Chest,
    Count      //How many types of inventory locations there are.
}

public enum ToolEffect
{
    None,
    Watering
}

public enum Direction
{
    Up,
    Down,
    Left,
    Right
}

public enum ItemType     //nice and general. Instead of Axe, we say chopping tool as can have multiple axe types.
{
    Seed,
    Commodity,
    WateringTool,
    HoeingTool,
    ChoppingTool,
    BreakingTool,
    ReapingTool,
    CollectingTool,
    ReapableScenery,
    Furniture,
    None,
    Count             //Can see how many item types there are.
}
