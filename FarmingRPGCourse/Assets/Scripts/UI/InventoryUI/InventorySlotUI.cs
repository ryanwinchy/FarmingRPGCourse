using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
                                             //Standard interfaces for mouse drag events.       //When mouse enters and exits the slot so we can show tooltip.                          
public class InventorySlotUI : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    Camera mainCamera;

    Canvas parentCanvas;
    [SerializeField] GameObject inventoryTextBoxPrefab;
    Transform parentItem;
    GameObject draggedItem;

    public Image inventorySlotHighlight;
    public Image inventorySlotImage;
    public TextMeshProUGUI textMeshProUGUI;
    [SerializeField] InventoryBarUI inventoryBar;
    [HideInInspector] public bool isSelected = false;
    [SerializeField] GameObject itemPrefab;

    [HideInInspector] public ItemDetails itemDetails;
    [HideInInspector] public int itemQuantity;
    [SerializeField] int slotNumber = 0;



    private void Awake()
    {
        parentCanvas = GetComponentInParent<Canvas>();
    }

    private void Start()
    {
        mainCamera = Camera.main;
        parentItem = GameObject.FindGameObjectWithTag(Tags.ITEMS_PARENT_TRANSFORM).transform;     //Have to do like this as is in different scene.
    }


    /// <summary>
    /// Sets this inventory slot as selected.
    /// </summary>
    void SetSelectedItem()   //called when click on inv slot.
    {
        //clear currently highlighted items.
        inventoryBar.ClearHighlightOnInventorySlots();

        //highlight this item on bar.
        isSelected = true;

        //set highlighted inventory on bar
        inventoryBar.SetHighlightedInventorySlots();

        //Set item selected in inventory.
        InventoryManager.Instance.SetSelectedInventoryItem(InventoryLocation.Player, itemDetails.itemCode);


        if (itemDetails.canBeCarried)    //show player carrying item (override animators to carry).
        {
            Player.Instance.ShowCarriedItem(itemDetails.itemCode);
        }
        else    //show player carrying nothing (set player animators to base).
        {
            Player.Instance.ClearCarriedItem();
        }
        
        
        
        
    }

    void ClearSelectedItem()
    {
        //clear currently highlighted.
        inventoryBar.ClearHighlightOnInventorySlots();

        isSelected = false;

        //set no item selected in inventory.
        InventoryManager.Instance.ClearSelectedInventoryItem(InventoryLocation.Player);


        //clear player carrying item visuals.
        Player.Instance.ClearCarriedItem();

    }


    /// <summary>
    /// Drops the item (if selected) at the current mouse position. Called by the DropItem event.
    /// </summary>
    private void DropSelectedItemAtMousePosition()
    {
        if (itemDetails != null && isSelected)        //Only drop if item was selected.
        {
            Vector3 worldPosition = mainCamera.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, -mainCamera.transform.position.z));    //Gets current mouse position in world space.

            //Create item from prefab at mouse pos.
            GameObject itemGameObject = Instantiate(itemPrefab, worldPosition, Quaternion.identity, parentItem);
            Item item = itemGameObject.GetComponent<Item>();
            item.ItemCode = itemDetails.itemCode;

            //Remove item from inventory.
            InventoryManager.Instance.RemoveItem(InventoryLocation.Player, item.ItemCode);

            //if no more of item, clear selected.
            if (InventoryManager.Instance.FindItemInInventory(InventoryLocation.Player, item.ItemCode) == -1)
            {
                ClearSelectedItem();
            }
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (itemDetails != null)
        {
            //Disable keyboard input.
            Player.Instance.DisablePlayerInputAndResetMovement();

            //Create a dragged item.
            draggedItem = Instantiate(inventoryBar.inventoryBarDraggedItem, inventoryBar.transform);

            //Get image component.
            Image draggedItemImage = draggedItem.GetComponentInChildren<Image>();
            draggedItemImage.sprite = inventorySlotImage.sprite;

            SetSelectedItem();      //So as we drag it always selects item.

        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        //move game object as drag item.
        if (draggedItem != null)
        {
            draggedItem.transform.position = Input.mousePosition;
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //Destroy game object dragged item.
        if (draggedItem != null)
        {
            Destroy(draggedItem);

            //if drag ends over inventory bar, get the item currently over and swap the items.
            if (eventData.pointerCurrentRaycast.gameObject != null && eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlotUI>() != null)
            {
                //Get the slot number of the inventory slot the mouse ended drag and swap them.

                int toSlotNumber = eventData.pointerCurrentRaycast.gameObject.GetComponent<InventorySlotUI>().slotNumber;

                //Swap the items.
                InventoryManager.Instance.SwapInventoryItems(InventoryLocation.Player, slotNumber, toSlotNumber);

                //Destroy the tool tip text box if exists.
                DestroyInventoryTextBox();

                //Clear selected item. Because we are swapping things so dont want anything selected.
                ClearSelectedItem();

            }
            else       //If dropping and not over inventory bar, attempt drop if it is droppable.
            {
                if (itemDetails.canBeDropped)
                {
                    DropSelectedItemAtMousePosition();
                }
            }

            //Enable keyboard input.
            Player.Instance.EnablePlayerInput();
        }
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //if left click.
        if (eventData.button == PointerEventData.InputButton.Left)
        {
            //if inv slot currently selected, deselect it.
            if (isSelected)
            {
                ClearSelectedItem();
            }
            else
            {
                //if inv slot not currently selected, select it.
                if (itemQuantity > 0)  //more than one so cant select empty slot.
                {
                    SetSelectedItem();
                }
            }
        }
    }
    
    public void OnPointerEnter(PointerEventData eventData)   //as we hover over slot, we see if slot has an item, we instantiate textbox. Save it in inventory text box var.
    {
        //Populate textbox with item details.
        if (itemQuantity != 0)
        {
            //Instantiate textbox.
            inventoryBar.inventoryTextBoxGameObject = Instantiate(inventoryTextBoxPrefab, transform.position, Quaternion.identity);     //instantiate and save in inventory bar script.
            inventoryBar.inventoryTextBoxGameObject.transform.SetParent(parentCanvas.transform, false);         //Set parent to parent canvas.

            InventoryTextBoxUI inventoryTextBoxUI = inventoryBar.inventoryTextBoxGameObject.GetComponent<InventoryTextBoxUI>();    //Store inventory text box component.

            //Set item type description.
            string itemTypeDescription = InventoryManager.Instance.GetItemTypeDescription(itemDetails.itemType);

            //populare text box.
            inventoryTextBoxUI.SetTextboxText(itemDetails.itemDescription, itemTypeDescription, "", itemDetails.itemLongDescription, "", "");

            //Set text box position according to bar (might be at top or bottom)
            if (inventoryBar.IsInventoryBarPositionBottom)
            {
                inventoryBar.inventoryTextBoxGameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 0f);      //pivot at bottom of text box.
                inventoryBar.inventoryTextBoxGameObject.transform.position = new Vector3(transform.position.x, transform.position.y + 50f, transform.position.z);
            }
            else
            {
                inventoryBar.inventoryTextBoxGameObject.GetComponent<RectTransform>().pivot = new Vector2(0.5f, 1f);      //pivot at top of text box.
                inventoryBar.inventoryTextBoxGameObject.transform.position = new Vector3(transform.position.x, transform.position.y - 50f, transform.position.z);
            }


        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        DestroyInventoryTextBox();
    }


    private void DestroyInventoryTextBox()
    {
        if (inventoryBar.inventoryTextBoxGameObject != null)
        {
            Destroy(inventoryBar.inventoryTextBoxGameObject);
        }
    }





}
