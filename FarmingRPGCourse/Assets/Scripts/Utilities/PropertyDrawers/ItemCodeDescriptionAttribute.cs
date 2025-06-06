using UnityEngine;


//This is a custom property drawer for the unity editor, like Range[] but our own one, so that when we type an item code in the inspector, it shows the item description.
public class ItemCodeDescriptionAttribute : PropertyAttribute
{
    //No values needed for this attribute, it just displays the item description, class can be empty.
}
