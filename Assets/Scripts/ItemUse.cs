using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//NIEUW
public class ItemUse : MonoBehaviour
{
    public void UseTool(Item item, GameObject Object)
    {
        if (item.itemName == "Tool")
        {
            Object.SetActive(false);
        }
    }
    public void UseFood(Item item)
    {
        if (item.itemName == "Apple")
        {

        }
    }
}
