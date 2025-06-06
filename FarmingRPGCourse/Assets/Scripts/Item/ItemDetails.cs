using UnityEngine;

[System.Serializable]   //So appears in inspector.
public class ItemDetails
{
    public string itemDescription;
    public int itemCode;
    public ItemType itemType;
    public Sprite itemSprite;
    public string itemLongDescription;
    public short itemUseGridRadius;    //short int.
    public float itemUseRadius;   //If not a grid based item, like a sythe looking for grass is not grid based.
    public bool isStartingItem;
    public bool canBePickedUp;   //eg an acorn can pikcup, but not a tree stump.
    public bool canBeDropped;   //eg starting tool can't be dropped but maybe pine cone can be.
    public bool canBeEaten;
    public bool canBeCarried;   //Carries above head.

}