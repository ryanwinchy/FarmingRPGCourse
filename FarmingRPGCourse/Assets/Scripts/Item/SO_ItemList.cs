using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "so_ItemList", menuName = "Scriptable Objects/Item/Item List")]   //Create in Assets/Scriptable Objects/Item.
public class SO_ItemList : ScriptableObject
{
    [SerializeField] public List<ItemDetails> itemDetailsList;



}