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
            foreach (InventorySlotUI inventorySlotUI in inventorySlotUIArray)   //Loop thru all inventory slots and clear them with blank sprite.
            {
                inventorySlotUI.inventorySlotImage.sprite = blank16x16Sprite;
                inventorySlotUI.textMeshProUGUI.text = "";
                inventorySlotUI.itemDetails = null;
                inventorySlotUI.itemQuantity = 0;
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

                        }

                    }
                    else          //Break when reach end. 
                        break;
                }
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
