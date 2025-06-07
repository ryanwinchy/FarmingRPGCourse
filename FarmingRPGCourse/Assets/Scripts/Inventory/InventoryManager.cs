using System.Collections.Generic;
using UnityEngine;
//DO NOT CODE INVENTORY LIKE THIS. IT'S STUPID. IS NOT OBJECT ORIENTED. ONE STATIC TO MANAGE ALL TYPES OF INVENTORIES, NEEDLESSLY CONFUSING AND WORSE RESULT. BETTER TO HAVE INVENTORY INSTANCES AND CAN CHANGE SIZE ETC... THIS IS DUMB.
//CONSTANTLY PASSING IN WHICH INVENTORY IT IS, INITIALISING ALL OF THEM. ITS LITERALLY WHAT INSTANCES ARE FOR. THIS IS NOT OBJECT ORIENTED.
//GENUINLEY I COULD CODE IT BETTER AND WAY MORE UNDERSTANDABLE. BUT GOOD TO FOLLOW TUT JUST TO SEE HOW HE DOES CERTAIN THINGS FOR 2D TOP DOWN RPG. LIKE LAYERING.
//MANAGING LISTS OF LISTS LIKE THIS IS JUST NEEDLESSLY CONFUSING, NOT CLEAN. WHOLE POINT OF OBJECT ORIENTED.
public class InventoryManager : SingletonMonobehaviour<InventoryManager>
{
    Dictionary<int, ItemDetails> itemDetailsDictionary;             //put code in and get the item details.

    public List<InventoryItem>[] inventoryLists;     //an array of inventory lists, one for each inventory location. index 0 is player, index 1 is chest.

    [HideInInspector] public int[] inventoryListCapacityIntArray; // aray of ints, the index is the inventory list we are specifying capacity for. eg 0 is player, 1 is chest.

    [SerializeField] SO_ItemList itemList = null;



    protected override void Awake()       //Self setup goes in awake, as other things will ref the item dictionary in their start.
    {
        base.Awake();    //Run the singleton instance stuff we always have.

        CreateInventoryLists();

        //Create item details dictionary
        CreateItemDetailsDictionary();
    }

    //ALSO NEEDLESSLY CONFUSING FOR A WORSE RESULT. NOT CLEAN. NOT OBJECT ORIENTED.
    private void CreateInventoryLists()    //THIS IS A STUPID WAY TO DO IT. GIIVNG TYPES OF INV AND GIVING SAME SIZE FOR ALL. NOT FLEXIBLE. SHOULD BE AN INSTANCE AND CAN CHOOSE SIZE IF IS CHEST OR SHOP OR SMTHN.
    {
        inventoryLists = new List<InventoryItem>[(int)InventoryLocation.Count];    //array of inventory lists, one for each inventory location (player, chest).

        for (int i = 0; i < (int)InventoryLocation.Count; i++)   //Loops thru array, creates a list of items for each (player, chest).
        {
            inventoryLists[i] = new List<InventoryItem>();
        }

        //Initialise the inventory list capacity array.
        inventoryListCapacityIntArray = new int[(int)InventoryLocation.Count];      //0 and 1 in the array.

        //Initialise the player inventory list capacity.
        inventoryListCapacityIntArray[(int)InventoryLocation.Player] = Settings.playerInitialInventoryCapacity;



    }


    /// <summary>
    /// Populates the itemDetailsDictionary from the itemList scriptable object items list in our files.
    /// </summary>
    private void CreateItemDetailsDictionary()
    {
        itemDetailsDictionary = new Dictionary<int, ItemDetails>();

        foreach (ItemDetails itemDetails in itemList.itemDetailsList)
        {
            itemDetailsDictionary.Add(itemDetails.itemCode, itemDetails);        //So from SO list we will have all the codes and corresponding details.
        }
    }


    /// <summary>
    /// Add an item to the inventory list for the inventory location and then destroy the game object.
    /// </summary>
    public void AddItem(InventoryLocation inventoryLocation, Item item, GameObject gameObjectToDestroy)
    {
        AddItem(inventoryLocation, item);
        Destroy(gameObjectToDestroy);
    }


    /// <summary>
    /// Add an item to the inventory list for the inventory location.
    /// </summary>
    public void AddItem(InventoryLocation inventoryLocation, Item item)   //Which inventory it is (player is 0), chest is 1. And item to add. Handles if exists in inventory already or not.
    {
        int itemCode = item.ItemCode;

        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];   //gets which inventory we are adding to.

        //Check if inventory already contains the item.
        int itemPosition = FindItemInInventory(inventoryLocation, itemCode);     //If is in inventory, returns position, if not returns -1.

        if (itemPosition != -1) //add new item.
        {
            AddItemAtPosition(inventoryList, item, itemPosition);
        }
        else       //add to existing item position.
        {
            AddItemAtPosition(inventoryList, item);
        }

        //Send event that inventory has been updated.
        EventHandler.CallInventoryUpdatedEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);
    }

    /// <summary>
    /// Swap item at fromItem with item ToItem index in inventory list. Swap UI slots basically of inv.
    /// </summary>
    public void SwapInventoryItems(InventoryLocation inventoryLocation, int fromItem, int toItem)
    {
        // if from index and toIndex are within bounds of list, not the same, and greater or equal to 0.
        if (fromItem < inventoryLists[(int)inventoryLocation].Count && toItem < inventoryLists[(int)inventoryLocation].Count && fromItem >= 0 && toItem >= 0 && fromItem != toItem)
        {
            //Get the items to swap.
            InventoryItem fromInventoryItem = inventoryLists[(int)inventoryLocation][fromItem];
            InventoryItem toInventoryItem = inventoryLists[(int)inventoryLocation][toItem];

            //Swap the items.
            inventoryLists[(int)inventoryLocation][fromItem] = toInventoryItem;
            inventoryLists[(int)inventoryLocation][toItem] = fromInventoryItem;

            //Send event that inventory has been updated so UI updates.
            EventHandler.CallInventoryUpdatedEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);
        }
    }

    /// <summary>
    /// Search specified inventory (player or chest) for an item code, if already there return pos in array, or -1 if not there.
    /// </summary>
    private int FindItemInInventory(InventoryLocation inventoryLocation, int itemCode)
    {
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];   //get which inventory we are searching.

        for (int i = 0; i < inventoryList.Count; i++)
        {
            if (inventoryList[i].itemCode == itemCode)
            {
                return i;
            }
        }
        return -1;
    }


    /// <summary>
    /// Add an item to the end of the inventory.
    /// </summary>
    private void AddItemAtPosition(List<InventoryItem> inventoryList, Item item)
    {
        InventoryItem inventoryItem = new InventoryItem();

        inventoryItem.itemCode = item.ItemCode;
        inventoryItem.itemQuantity = 1;   //as is the first, we didn't have any. SHOULD BE ABLE TO ADD STACKS THO.

        inventoryList.Add(inventoryItem);


        //DebugPrintInventoryList(inventoryList);

    }

    /// <summary>
    /// Add item to specified position in inventory list.
    /// </summary>
    private void AddItemAtPosition(List<InventoryItem> inventoryList, Item item, int position)
    {
        InventoryItem inventoryItem = new InventoryItem();

        int quantity = inventoryList[position].itemQuantity + 1;     //Already there, so add 1 to quantity.

        inventoryItem.itemCode = item.ItemCode;
        inventoryItem.itemQuantity = quantity;

        inventoryList[position] = inventoryItem;       //the list at that pos is replaced with new item quantity.


        //DebugPrintInventoryList(inventoryList);

    }




    /// <summary>
    /// Returns the item details (from SO list) for the given item code, or null if the item code doesn't exist.
    /// </summary>

    public ItemDetails GetItemDetails(int itemCode)
    {
        ItemDetails itemDetails;

        if (itemDetailsDictionary.TryGetValue(itemCode, out itemDetails))    //If item code is in dictionary assign to itemDetails and return it.
        {
            return itemDetails;
        }
        else   //No code in dictionary.
        {
            return null;
        }
    }


    /// <summary>
    /// Get the item type description for an itep type - returns the item type description as a string for a given item type.
    /// </summary>
    public string GetItemTypeDescription(ItemType itemType)
    {
        string itemTypeDescription;

        switch (itemType)
        {
            case ItemType.BreakingTool:
                itemTypeDescription = Settings.BREAKING_TOOL;   //Cleaner than dealing with strings directly.
                break;
            case ItemType.ChoppingTool:
                itemTypeDescription = Settings.CHOPPING_TOOL;
                break;
            case ItemType.HoeingTool:
                itemTypeDescription = Settings.HOEING_TOOL;
                break;
            case ItemType.ReapingTool:
                itemTypeDescription = Settings.REAPING_TOOL;
                break;
            case ItemType.WateringTool:
                itemTypeDescription = Settings.WATERING_TOOL;
                break;
            default:
                itemTypeDescription = itemType.ToString();
                break;
        }

        return itemTypeDescription;
    }




    /// <summary>
    /// Remove an item from the inventory, and create a game object at the mouse position it was dropped at.
    /// </summary>
    public void RemoveItem(InventoryLocation inventoryLocation, int itemCode)
    {
        List<InventoryItem> inventoryList = inventoryLists[(int)inventoryLocation];    //Get the correct inventory list. Player is 0.

        //Check if inventory already contains the item.
        int itemPosition = FindItemInInventory(inventoryLocation, itemCode);

        if (itemPosition != -1)   //if item was in inventory.
        {
            RemoveItemAtPosition(inventoryList, itemCode, itemPosition);
        }

        //Send event that inventory has been updated.
        EventHandler.CallInventoryUpdatedEvent(inventoryLocation, inventoryLists[(int)inventoryLocation]);
    }

    void RemoveItemAtPosition(List<InventoryItem> inventoryList, int itemCode, int position)
    {
        InventoryItem inventoryItem = new InventoryItem();

        int quantity = inventoryList[position].itemQuantity - 1;    //Consume one.

        if (quantity > 0)      //One consumed but still some stack in the inventory.
        {
            inventoryItem.itemQuantity = quantity;
            inventoryItem.itemCode = itemCode;
            inventoryList[position] = inventoryItem;
        }
        else
        {
            inventoryList.RemoveAt(position);
        }
    }



    // private void DebugPrintInventoryList(List<InventoryItem> inventoryList)
    // {
    //     Debug.ClearDeveloperConsole();

    //     foreach (InventoryItem inventoryItem in inventoryList)
    //     {
    //         Debug.Log("Item Description: " + InventoryManager.Instance.GetItemDetails(inventoryItem.itemCode).itemDescription + " Item Quantity: " + inventoryItem.itemQuantity);
    //     }

    //     Debug.Log("****************************************************************************************");
    // }




}
