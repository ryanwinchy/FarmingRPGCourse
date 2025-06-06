using UnityEngine;

public class ItemPickup : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Item item = collision.GetComponent<Item>();

        if (item != null)     //We triggered with an item.
        {
            //Get item details
            ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(item.ItemCode);

            //Print item details to console
            Debug.Log(itemDetails.itemDescription);
                
        }
        
    }

    
}
