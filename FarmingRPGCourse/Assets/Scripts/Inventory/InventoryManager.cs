using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : SingletonMonobehaviour<InventoryManager>
{
    Dictionary<int, ItemDetails> itemDetailsDictionary;             //put code in and get the item details.

    [SerializeField] SO_ItemList itemList = null;



    void Start()
    {
        //Create item details dictionary
        CreateItemDetailsDictionary();
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
    



}
