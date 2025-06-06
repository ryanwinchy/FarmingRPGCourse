using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

[CustomPropertyDrawer(typeof(ItemCodeDescriptionAttribute))]
public class ItemCodeDescriptionDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label)   //We need to change the returned property height to be double because we are showing two things (item code and item description).
    {
        return EditorGUI.GetPropertyHeight(property) * 2;
    }

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)  //Using BeginProperty and EndProperty on the parent property means that the prefab override logic works on the entire property.
    {
        EditorGUI.BeginProperty(position, label, property);

        if (property.propertyType == SerializedPropertyType.Integer)    //Check if the property is an integer.
        {
            EditorGUI.BeginChangeCheck();     //Start check changes of values in the editor and then applies them at the end of the function.

            //Draw item code.
            int newValue = EditorGUI.IntField(new Rect(position.x, position.y, position.width, position.height / 2), label, property.intValue);    //Only half the height as is only one line. then draw the value.

            //Draw item description.
            EditorGUI.LabelField(new Rect(position.x, position.y + position.height / 2, position.width, position.height / 2), "Item Description: ", GetItemDescription(property.intValue));
                                                          //y offset so is above.

            //if item code value has changed, then set value to new value.
            if (EditorGUI.EndChangeCheck())
            {
                property.intValue = newValue;
            }
        }


        EditorGUI.EndProperty();
    }

    private string GetItemDescription(int itemCode)    //Returns string item description from item code.
    {
        SO_ItemList soItemList;

        soItemList = AssetDatabase.LoadAssetAtPath<SO_ItemList>("Assets/ScriptableObjects/Item/so_ItemList.asset");      //This method actually queries files within our assets folder.

        List<ItemDetails> itemDetailsList = soItemList.itemDetailsList;

        ItemDetails itemDetails = itemDetailsList.Find(x => x.itemCode == itemCode);     //Find x where x item code matches the item code passed in to the method. So finds the item detail of the code. Or null if not found.

        if (itemDetails != null)
        {
            return itemDetails.itemDescription;
        }
        else
        {
            return "Item not found";
        }
    }

}
