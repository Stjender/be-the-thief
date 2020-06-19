using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NIEUW
public class ItemUse : MonoBehaviour
{
    public void UseTool(Item item, GameObject Object)
    {
        if (item.itemType == ItemTypes.Tool)
        {
            if (Object.GetComponent<Door>() != null)
            {
                Object.GetComponent<Door>().openDoor();
            }
        }
    }
    public void UseFood(Item item)
    {
        if (item.itemName == "Apple")
        {

        }
    }
}