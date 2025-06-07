[System.Serializable]      //As want to serialize inventory items when do things like save game.
public struct InventoryItem
{
    public int itemCode;
    public int itemQuantity;
}