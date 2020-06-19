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
                if (!Object.GetComponent<Door>().open)
                {
                    item.animator.SetTrigger("activate");
                    Object.GetComponent<Door>().openDoor();
                }
                else
                {
                    Object.GetComponent<Door>().openDoor();
                }
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