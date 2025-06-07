
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
