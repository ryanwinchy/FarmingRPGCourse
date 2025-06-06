using UnityEngine;

public class Item : MonoBehaviour
{
    [ItemCodeDescription]        //Editor attribute we made to simply show the item description in inspector when you have the item code.
    [SerializeField] private int itemCode;

    public int ItemCode { get { return itemCode; } set { itemCode = value; } }    //property because itemCode is private SF , and cant SF properties.

    SpriteRenderer spriteRenderer;

    private void Awake()
    {
        spriteRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    void Start()
    {
        if (ItemCode != 0)
        {
            Init(ItemCode);
        }
    }

    public void Init(int itemCodeParam)
    {
        
    }
    
}
