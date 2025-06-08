using System.Collections.Generic;
using UnityEngine;

public class InventoryBarUI : MonoBehaviour
{

    [SerializeField] Sprite blank16x16Sprite;
    [SerializeField] InventorySlotUI[] inventorySlotUIArray = null;

    public GameObject inventoryBarDraggedItem;
    [HideInInspector] public GameObject inventoryTextBoxGameObject = null;
    RectTransform rectTransform;

    public bool IsInventoryBarPositionBottom { get; set; } = true;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void OnEnable()
    {
        EventHandler.OnInventoryUpdatedEvent += InventoryUpdated_UpdateVisual;
    }

    void OnDisable()
    {
        EventHandler.OnInventoryUpdatedEvent -= InventoryUpdated_UpdateVisual;
    }
    

    private void Update()
    {
        SwitchInventoryBarPosition();
    }
    
    private void ClearInventorySlots()
    {
        if (inventorySlotUIArray.Length > 0)
        {
            for (int i = 0; i < inventorySlotUIArray.Length; i++)   //Loop thru all inventory slots and clear them with blank sprite.
            {
                inventorySlotUIArray[i].inventorySlotImage.sprite = blank16x16Sprite;
                inventorySlotUIArray[i].textMeshProUGUI.text = "";
                inventorySlotUIArray[i].itemDetails = null;
                inventorySlotUIArray[i].itemQuantity = 0;

                SetHighlightedInventorySlots(i);       //Clears highlight or selects if selected.
            }
        }
    }

    /// <summary>
    /// Clear all highlights from inventory bar.
    /// </summary>
    public void ClearHighlightOnInventorySlots()
    {
        if (inventorySlotUIArray.Length > 0)
        {
            for (int i = 0; i < inventorySlotUIArray.Length; i++)   //Loop thru all slots and clear highlights.
            {
                if (inventorySlotUIArray[i].isSelected)
                {
                    inventorySlotUIArray[i].isSelected = false;
                    inventorySlotUIArray[i].inventorySlotHighlight.color = new Color(0f, 0f, 0f, 0f);

                    //update inventory as item not selected.              //BAD CODE - SHOULD BE EVENTS MAKING THINGS HAPPEN SO DECOUPLED.
                    InventoryManager.Instance.ClearSelectedInventoryItem(InventoryLocation.Player);
                }
            }
        }
    }

    private void InventoryUpdated_UpdateVisual(InventoryLocation inventoryLocation, List<InventoryItem> inventoryList)
    {
        if (inventoryLocation == InventoryLocation.Player)
        {
            ClearInventorySlots();

            if (inventorySlotUIArray.Length > 0 && inventoryList.Count > 0)      //Ui has slots and player has anything in inventory.
            {
                //Loop thru inventory slots and update with the corresponding inventory item.
                for (int i = 0; i < inventorySlotUIArray.Length; i++)
                {
                    if (i < inventoryList.Count)
                    {
                        int itemCode = inventoryList[i].itemCode;

                        ItemDetails itemDetails = InventoryManager.Instance.GetItemDetails(itemCode);

                        if (itemDetails != null)
                        {
                            //Add images and item details to inventory item slot.
                            inventorySlotUIArray[i].inventorySlotImage.sprite = itemDetails.itemSprite;
                            inventorySlotUIArray[i].textMeshProUGUI.text = inventoryList[i].itemQuantity.ToString();
                            inventorySlotUIArray[i].itemDetails = itemDetails;
                            inventorySlotUIArray[i].itemQuantity = inventoryList[i].itemQuantity;

                            //Set highlighted inventory slots as inventory updated.
                            SetHighlightedInventorySlots(i);

                        }

                    }
                    else          //Break when reach end. 
                        break;
                }
            }
        }

    }

    /// <summary>
    /// Set the selected highlight by looping thru all and highlighting selected.
    /// </summary>
    public void SetHighlightedInventorySlots()
    {
        if (inventorySlotUIArray.Length > 0)
        {
            //loop thru all inventory slots and clear highlights.

            for (int i = 0; i < inventorySlotUIArray.Length; i++)
            {
                SetHighlightedInventorySlots(i);
            }
        }
    }

    void SetHighlightedInventorySlots(int itemPosition)
    {
        if (inventorySlotUIArray.Length > 0 && inventorySlotUIArray[itemPosition].itemDetails != null)
        {
            if (inventorySlotUIArray[itemPosition].isSelected)
            {
                inventorySlotUIArray[itemPosition].inventorySlotHighlight.color = new Color(1f, 1f, 1f, 1f);

                //update inventory to show item selected.
                InventoryManager.Instance.SetSelectedInventoryItem(InventoryLocation.Player, inventorySlotUIArray[itemPosition].itemDetails.itemCode);
            }
            
        }
    }


    private void SwitchInventoryBarPosition()
    {
        Vector3 playerViewportPosition = Player.Instance.GetPlayerViewportPosition();    //is always getting players position in viewport (screen) so we know when near bottom of screen.

        if (playerViewportPosition.y > 0.3f && IsInventoryBarPositionBottom == false)  //If up 1/3 of screen and bar is not on bottom, move to bottom.
        {

            rectTransform.pivot = new Vector2(0.5f, 0);       //Set rect transform details to move to bottom. Was this by default when we set UI to bottom in editor.
            rectTransform.anchorMin = new Vector2(0.5f, 0);
            rectTransform.anchorMax = new Vector2(0.5f, 0);
            rectTransform.anchoredPosition = new Vector2(0, 2.5f);

            IsInventoryBarPositionBottom = true;
        }
        else if (playerViewportPosition.y <= 0.3f && IsInventoryBarPositionBottom == true)  //If down 1/3 of screen and bar is on bottom, move to top.
        {
            rectTransform.pivot = new Vector2(0.5f, 1);       //Set rect transform details to move to top.
            rectTransform.anchorMin = new Vector2(0.5f, 1);
            rectTransform.anchorMax = new Vector2(0.5f, 1);
            rectTransform.anchoredPosition = new Vector2(0, -2.5f);

            IsInventoryBarPositionBottom = false;
        }

    }













}
